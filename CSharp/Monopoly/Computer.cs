using System.Drawing;

namespace Monopoly
{
    public class Computer : Player
    {
        public Color Color { get; }
        
        public Computer(Color color)
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