namespace Monopoly.UnitTests
{
    public class TestPlayer : Player
    {
        public  bool ShouldSell { get; set; }
        public bool ShouldBuy { get; set; }

        public TestPlayer(decimal startMoney)
            : base(startMoney)
        {
            ShouldSell = false;
            ShouldBuy = false;
        }

        public override bool WouldLikeToSell(Property property)
        {
            return ShouldSell;
        }

        public override bool WouldLikeToBuy(Property property)
        {
            return ShouldBuy;
        }
    }
}