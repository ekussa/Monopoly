namespace Monopoly
{
    public class Company : Property
    {
        public decimal Multiplier { get; }
        
        public Company(string name, decimal buyPrice, decimal mortgage, decimal multiplier)
            : base(name, buyPrice, mortgage)
        {
            Multiplier = multiplier;
        }
    }
}