using System.Collections.Generic;
using System.Linq;

namespace Monopoly
{
    public class PlayerCursor
    {
        private const int SameDiceMax = 3;
        
        private readonly IList<Player> _players;
        private readonly IDice _dice;
        private int _currentPlayer;
        private bool _goToNextPlayer;
        private int _goToJailCounter;

        public PlayerCursor(IList<Player> players, IDice dice)
        {
            _dice = dice;
            _players = players;
            _currentPlayer = 0;
            _goToNextPlayer = true;
            _goToJailCounter = SameDiceMax;
        }

        private void TickIndex()
        {
            if (_currentPlayer >= _players.Count - 1)
                _currentPlayer = 0;
            else
                _currentPlayer++;
        }

        private Player GetPlayer()
        {
            return _players[_currentPlayer];
        }

        private bool GoToJail(IReadOnlyList<int> diceResult)
        {
            if (diceResult[0] == diceResult[1])
            {
                _goToNextPlayer = false;
                return --_goToJailCounter == 0;
            }

            _goToNextPlayer = true;
            return false;
        }
        
        public PlayerTurn RollDice()
        {
            var dice = _dice.RollTwice();
            var shouldGoToJail = GoToJail(dice);
            var player = GetPlayer();

            if (shouldGoToJail || _goToNextPlayer)
            {
                _goToJailCounter = SameDiceMax;
                TickIndex();
            }
            
            return
                new PlayerTurn(
                    player,
                    dice.Sum(),
                    shouldGoToJail);
        }
    }
}