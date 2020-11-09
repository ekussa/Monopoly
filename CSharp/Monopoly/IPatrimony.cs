namespace Monopoly
{
    public interface IPatrimony
    {
        public decimal Cash { get; set; }
        bool Exchange(Property sellerProperty, IPatrimony buyer);

        void Credit(decimal cash);
        bool Debit(decimal cash);
        
        void Credit(Property property);
        bool Debit(Property property);
        int Count { get; }
    }
}