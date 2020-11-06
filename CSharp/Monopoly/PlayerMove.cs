namespace Monopoly
{
    public class PlayerMove
    {
        public Player Player { get; }
        public int Total { get; }
        public bool Unfrozen { get; }

        public PlayerMove(Player player, int total, bool unfrozen)
        {
            Player = player;
            Total = total;
            Unfrozen = unfrozen;
        }
    }
}