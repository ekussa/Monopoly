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

        private int SingleRoll()
        {
            return _random.Next(1, 7);
        }
        
        public int[] Roll()
        {
            _lastRoll = new[]
            {
                SingleRoll(),
                SingleRoll()
            };
            return _lastRoll;
        }

        public int[] LastRoll()
        {
            return _lastRoll;
        }
    }
}