namespace Monopoly
{
    public class SpendChangeCard : ChanceCard
    {
        public decimal Value { get; }
        
        public SpendChangeCard(decimal value, string text)
            : base(text)
        {
            Value = value;
        }
    }
}