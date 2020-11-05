using System;

namespace Monopoly
{
    public class Dice : IDice
    {
        private readonly Random _random;

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
            return
                new[]
                {
                    Throw(),
                    Throw()
                };
        }
    }
}