using System.Collections.Generic;

namespace Monopoly
{
    public class BoardCursor
    {
        private readonly Board _board;
        private readonly IPlayerCursor _playerCursor;
        
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
        }

        private void SendPlayerToStart(Player player)
        {
            _positions[player] = 0;
        }

        private void SendPlayerToPrison(Player player)
        {
            _positions[player] = 0;
        }

        private void MovePlayerTo(Player player, int places)
        {
            var position = _positions[player] + places;
            _positions[player] = position % _board.Count;
        }
        
        public void NextTurn()
        {
            var nextMove = _playerCursor.Next();
            if (nextMove.Unfrozen)
                SendPlayerToPrison(nextMove.Player);
            else
                MovePlayerTo(nextMove.Player, nextMove.Total);
        }

        private int GetPrisonVisitIndex()
        {
            for (var i = 0; i < _board.Count; i++)
            {
                if(_board[i].GetType() != typeof(PrisonVisit))
                    continue;
                return i;
            }

            throw new PrisonNotFound();
        }
    }
}