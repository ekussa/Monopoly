using System.Collections.Generic;
using System.Linq;

namespace Monopoly
{
    public class BoardCursor : SquareActions
    {
        private const decimal TwoHundredCash = 200m;
        
        private readonly Board _board;
        private readonly IPlayerCursor _playerCursor;

        private int _freezeIndex;

        public Dictionary<Player, int> Positions { get; }

        public BoardCursor(Board board, IPlayerCursor playerCursor)
        {
            _board = board;
            _playerCursor = playerCursor;
            
            var players = _playerCursor.GetAll();
            Positions = new Dictionary<Player, int>(players.Count);
            foreach (var player in players)
                SendToStart(player);

            _freezeIndex = -1;
        }

        private void SendToStart(Player player)
        {
            Positions[player] = 0;
        }

        private void SendToPrison(Player player)
        {
            if (_freezeIndex == -1)
                _freezeIndex = GetPrisonVisitIndex();
            Positions[player] = _freezeIndex;
        }

        private void MovePlayerTo(Player player, int places)
        {
            var position = Positions[player] + 1;
            for (var i = 0; i < places - 1; i++)
            {
                _board[position].OnPass?.Invoke(_board[position], player);
                position = ++position % _board.Count;
            }
            _board[position].OnStop?.Invoke(_board[position], player);
            Positions[player] = position % _board.Count;
        }
        
        public void NextTurn()
        {
            var nextMove = _playerCursor.Next();
            if (nextMove.Frozen)
                SendToPrison(nextMove.Player);
            else
                MovePlayerTo(nextMove.Player, nextMove.Total);
        }

        private int GetPrisonVisitIndex()
        {
            for (var i = 0; i < _board.Count; i++)
            {
                if(_board[i].GetType() != typeof(FreezeVisit))
                    continue;
                return i;
            }

            return 0;
        }

        protected override void OnStartLandStop(Player player, Square square)
        {
            player.Patrimony.Credit(TwoHundredCash);
        }

        protected override void OnCommunityChestStop(Player player, Square square)
        {
            player.Patrimony.Credit(TwoHundredCash);
        }

        protected override void OnStartLandPass(Player player, Square square)
        {
            player.Patrimony.Credit(TwoHundredCash);
        }

        protected override void OnTheftStop(Player player, Square square)
        {
            player.Patrimony.Debit(TwoHundredCash);
        }

        protected override void OnFreezeEntryStop(Player player, Square square)
        {
            SendToPrison(player);
        }

        protected override void OnChanceSquareStop(Player player, Square square)
        {
            
        }

        protected override void OnLandStop(Player player, Square square)
        {
            var land = (Land) square;
            var owner = _playerCursor.GetOwnerOf(land);
            if (owner.WouldLikeToSell(land) && player.WouldLikeToBuy(land))
            {
                player.Buy(land, player);
                return;
            }

            var transaction = land.GetRent;
            player.
                Patrimony.
                Debit(transaction);
            
            owner.
                Patrimony.
                Credit(transaction);
        }

        protected override void OnCompanyStop(Player player, Square square)
        {
            var company = (Company) square;
            var owner = _playerCursor.GetOwnerOf(company);
            if (owner.WouldLikeToSell(company) && player.WouldLikeToBuy(company))
            {
                player.Buy(company, player);
                return;
            }

            var dice = _playerCursor.Dice.LastRoll().Sum();
            var transaction = company.GetBill(dice);
            player.
                Patrimony.
                Debit(transaction);
            
            owner.
                Patrimony.
                Credit(transaction);
        }
    }
}