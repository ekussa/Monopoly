namespace Monopoly
{
    public class Company : Property
    {
        public decimal Multiplier { get; }
        
        public Company(string name, decimal buyPrice, decimal multiplier)
            : base(name, buyPrice)
        {
            Multiplier = multiplier;
        }

        public decimal GetBill(int dice)
        {
            return Multiplier * dice;
        }
    }
}