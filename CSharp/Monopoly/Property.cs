namespace Monopoly
{
    public abstract class Property : Square
    {
        public decimal BuyPrice { get; }

        protected Property(string name, decimal buyPrice)
            : base(name)
        {
            BuyPrice = buyPrice;
        }
    }
}