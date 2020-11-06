using System;
using System.Collections.Generic;
using System.Linq;

namespace Monopoly
{
    public class PlayerCursor : IPlayerCursor
    {
        private readonly List<Player> _players;
        private readonly IDice _dice;
        
        private int _currentPlayer;

        public PlayerCursor(List<Player> players, IDice dice)
        {
            _dice = dice;
            _players = players;

            _currentPlayer = 0;
        }

        private void TickIndex()
        {
            if (_currentPlayer >= _players.Count - 1)
                _currentPlayer = 0;
            else
                _currentPlayer++;
        }

        public PlayerMove Next()
        {
            var dices = _dice.Roll();
            var player = _players[_currentPlayer];

            var movePerm = player.Mobility.CanMove(dices);
            if(movePerm != MovementResult.CanMoveSamePlayer)
                TickIndex();

            return movePerm switch
            {
                MovementResult.CanMove => player.Move(dices.Sum()),
                MovementResult.CanMoveSamePlayer => player.Move(dices.Sum()),
                MovementResult.CannotMove => player.DontMove(),
                MovementResult.JustFrozen => player.Freeze(),
                MovementResult.ExpiredUnfreeze => player.Move(dices.Sum()),
                _ => throw new ArgumentOutOfRangeException()
            };
        }

        public List<Player> GetAll()
        {
            return _players;
        }
    }
}