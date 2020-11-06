namespace Monopoly
{
    public class PlayerMove
    {
        public Player Player { get; }
        public int Total { get; }
        public bool Frozen { get; }

        public PlayerMove(Player player, int total, bool frozen = false)
        {
            Player = player;
            Total = total;
            Frozen = frozen;
        }
    }
}