namespace Monopoly
{
    public abstract class Player
    {
        private const int SameDiceMax = 3;
        private const int UnfreezeAttempts = 4;
        
        public Mobility Mobility { get; }
        public IPatrimony Patrimony { get; }

        protected Player(decimal cash)
        {
            Mobility = new Mobility(SameDiceMax, UnfreezeAttempts);
            Patrimony = new Patrimony(cash);
        }

        public void Buy(Property property, Player seller)
        {
            Patrimony.Exchange(property, seller.Patrimony);
        }
        
        public void Buy(Property property, decimal price, Player seller)
        {
            Patrimony.Exchange(property, price, seller.Patrimony);
        }

        public abstract bool WouldLikeToSell(Property property);
        public abstract bool WouldLikeToBuy(Property property);
    }
}