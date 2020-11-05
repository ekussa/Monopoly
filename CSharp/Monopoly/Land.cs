using System.Drawing;

namespace Monopoly
{
    public class Land : Property
    {
        public Color Color;
        public RentPrice RentPrice { get; set; }
        public decimal EnhancementPrice { get; set; }
        public LandEnhancements LandEnhancements { get; set; }

        public Land(string name, decimal buyPrice, decimal mortgage, Color color) :
            base(name, buyPrice, mortgage)
        {
            Color = color;
        }
    }
}