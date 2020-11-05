namespace Monopoly
{
    public class PlayerTurn
    {
        public Player Player { get; }
        public decimal Total { get; }
        public bool ShouldGoToJail { get; }

        public PlayerTurn(Player player, decimal total, bool goToJail)
        {
            Player = player;
            Total = total;
            ShouldGoToJail = goToJail;
        }
    }
}