using System;
using System.Collections.Generic;
using System.Linq;

namespace Monopoly
{
    public class PlayerCursor : IPlayerCursor
    {
        private readonly List<Player> _players;
        
        private int _currentPlayer;

        public IDice Dice { get; set; }

        public PlayerCursor(List<Player> players, IDice dice)
        {
            Dice = dice;
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
            var dices = Dice.Roll();
            var player = _players[_currentPlayer];

            var movePerm = player.Mobility.CanMove(dices);
            if(movePerm != MovementResult.CanMoveSamePlayer)
                TickIndex();

            return movePerm switch
            {
                MovementResult.CanMove => player.Move(dices.Sum()),
                MovementResult.ExpiredUnfreeze => player.Move(dices.Sum()),
                MovementResult.CanMoveSamePlayer => player.Move(dices.Sum()),
                MovementResult.CannotMove => player.CannotMove(),
                MovementResult.JustFrozen => player.Freeze(),
                _ => throw new ArgumentOutOfRangeException()
            };
        }

        public Player GetOwnerOf(Property property)
        {
            return
                _players.
                    FirstOrDefault(player =>
                        player.Patrimony.Owns(property));
        }

        public List<Player> GetAll()
        {
            return _players;
        }
    }
}