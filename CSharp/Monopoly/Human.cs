using System.Drawing;

namespace Monopoly
{
    public class Human : Player
    {
        public Color Color { get; }
        
        public Human(Color color)
        {
            Color = color;
        }
    }
}