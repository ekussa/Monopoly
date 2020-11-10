using System;

namespace Monopoly
{
    public class Dice : IDice
    {
        private readonly Random _random;
        private int[] _lastRoll;

        public Dice()
        {
            _random = new Random(DateTime.Now.Millisecond);
        }

        private int Throw()
        {
            return _random.Next(0, 7);
        }
        
        public int[] Roll()
        {
            _lastRoll = new[]
            {
                Throw(),
                Throw()
            };
            return _lastRoll;

        }

        public int[] LastRoll()
        {
            return _lastRoll;
        }
    }
}