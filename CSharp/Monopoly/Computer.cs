using System.Drawing;

namespace Monopoly
{
    public class Computer : Player
    {
        public Color Color { get; }
        
        public Computer(Color color)
        {
            Color = color;
        }
    }
}