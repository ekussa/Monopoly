namespace Monopoly
{
    public abstract class Account : IAccount<decimal>
    {
        private decimal _value;

        protected Account(decimal value)
        {
            _value = value;
        }

        public decimal Debit(decimal value)
        {
            if (value > _value)
            {
                var ret = _value;
                _value = 0;
                return ret;
            }

            _value -= value;
            return _value;
        }
        
        public decimal Credit(decimal value)
        {
            _value += value;
            return _value;
        }
    }
}