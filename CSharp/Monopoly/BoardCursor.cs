using System.Collections.Generic;

namespace Monopoly
{
    public class BoardCursor
    {
        private readonly Board _board;
        private readonly IPlayerCursor _playerCursor;
        private int _freezeIndex;
        
        private Dictionary<Player, int> _positions;
        public Dictionary<Player, int> Positions => _positions;

        public BoardCursor(Board board, IPlayerCursor playerCursor)
        {
            _board = board;
            _playerCursor = playerCursor;
            
            var players = _playerCursor.GetAll();
            _positions = new Dictionary<Player, int>(players.Count);
            foreach (var player in players)
                SendPlayerToStart(player);

            _freezeIndex = -1;
        }

        private void SendPlayerToStart(Player player)
        {
            _positions[player] = 0;
        }

        private void SendPlayerToPrison(Player player)
        {
            if (_freezeIndex != -1) return;
            
            _freezeIndex = GetPrisonVisitIndex();
            _positions[player] = _freezeIndex;
        }

        private void MovePlayerTo(Player player, int places)
        {
            var position = _positions[player] + places;
            _positions[player] = position % _board.Count;
        }
        
        public void NextTurn()
        {
            var nextMove = _playerCursor.Next();
            if (nextMove.Frozen)
                SendPlayerToPrison(nextMove.Player);
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
    }
}