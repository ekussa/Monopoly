using System.Drawing;

namespace Monopoly
{
    public class Human : Player
    {
        public Color Color { get; }
        
        public Human(Color color)
            : base(2558000m)
        {
            Color = color;
        }

        public override bool WouldLikeToSell(Property property)
        {
            return false;
        }

        public override bool WouldLikeToBuy(Property property)
        {
            return false;
        }
    }
}