namespace Monopoly
{
    public class EarnChangeCard : ChanceCard
    {
        public decimal Value { get; }
        
        public EarnChangeCard(decimal value, string text) :
            base(text)
        {
            Value = value;
        }
    }
}