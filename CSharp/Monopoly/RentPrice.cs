namespace Monopoly
{
    public class RentPrice
    {
        private readonly decimal[] _rent;

        public RentPrice(
            decimal rent,
            decimal rentOneHouse,
            decimal rentTwoHouses,
            decimal rentThreeHouses,
            decimal rentFourHouses,
            decimal rentHotel)
        {
            _rent = new[]
            {
                rent,
                rentOneHouse,
                rentTwoHouses,
                rentThreeHouses,
                rentFourHouses,
                rentHotel
            };
        }

        public decimal GetRent(LandEnhancements landEnhancements)
        {
            return _rent[(int)landEnhancements];
        }
    }
}