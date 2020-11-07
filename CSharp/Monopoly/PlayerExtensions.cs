namespace Monopoly
{
    public static class PlayerExtensions
    {
        public static PlayerMove Move(this Player player, int total)
        {
            return new PlayerMove(player, total);
        }
        
        public static PlayerMove CannotMove(this Player player)
        {
            return new PlayerMove(player, 0);
        }

        public static PlayerMove Freeze(this Player player)
        {
            return new PlayerMove(player, 0, true);
        }
    }
}