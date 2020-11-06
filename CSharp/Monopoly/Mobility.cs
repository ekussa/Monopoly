using System.Collections.Generic;
using System.Linq;

namespace Monopoly
{
    public class Mobility
    {
        private readonly int _unfreezeAttempts;
        private readonly int _sameDiceMax;

        private int _movementCount;
        private bool Frozen => _movementCount < 0;

        public Mobility(int sameDiceMax, int unfreezeAttempts)
        {
            _sameDiceMax = sameDiceMax;
            _unfreezeAttempts = unfreezeAttempts;
        }
        
        public MovementResult CanMove(IEnumerable<int> dices)
        {
            if (AreDicesSame(dices))
            {
                if (Frozen)
                {
                    Unfreeze();
                    return MovementResult.CanMove;
                }

                IncrementSpeedMovement();
                return Frozen ?
                    MovementResult.JustFrozen :
                    MovementResult.CanMoveSamePlayer;
            }
            
            if (!Frozen)
                return MovementResult.CanMove;

            IncrementToUnfreeze();
            return Frozen ?
                MovementResult.CannotMove :
                MovementResult.ExpiredUnfreeze;
        }

        private void Unfreeze()
        {
            _movementCount = 0;
        }

        private void Freeze()
        {
            _movementCount = -_unfreezeAttempts;
        }

        private void IncrementSpeedMovement()
        {
            if(++_movementCount >= _sameDiceMax)
                Freeze();
        }

        private void IncrementToUnfreeze()
        {
            ++_movementCount;
        }
        
        private static bool AreDicesSame(IEnumerable<int> dices)
        {
            var dicesList = dices.ToList();
            return dicesList.Count > 1 &&
                   dicesList.GroupBy(_ => _).Count() == 1;
        }
    }
}