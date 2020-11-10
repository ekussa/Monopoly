
namespace Monopoly
{
    public class Bank : Player
    {
        public Bank()
            : base(9000000m)
        {
        }

        public override bool WouldLikeToSell(Property property)
        {
            return true;
        }

        public override bool WouldLikeToBuy(Property property)
        {
            return false;
        }
    }
}