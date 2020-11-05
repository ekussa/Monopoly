namespace Monopoly
{
    public abstract class Property : Square
    {
        public decimal BuyPrice { get; }
        public decimal Mortgage { get; }

        protected Property(string name, decimal buyPrice, decimal mortgage)
            : base(name)
        {
            BuyPrice = buyPrice;
            Mortgage = mortgage;
        }
    }
}