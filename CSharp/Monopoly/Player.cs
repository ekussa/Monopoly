namespace Monopoly
{
    public abstract class Player
    {
        private const int SameDiceMax = 3;
        private const int UnfreezeAttempts = 4;
        
        public Mobility Mobility { get; }

        protected Player()
        {
            Mobility = new Mobility(SameDiceMax, UnfreezeAttempts);
        }
        
        public PlayerMove Move(int total)
        {
            return new PlayerMove(this, total);
        }
        
        public PlayerMove DontMove()
        {
            return new PlayerMove(this, 0);
        }

        public PlayerMove Freeze()
        {
            return new PlayerMove(this, 0, true);
        }
    }
}