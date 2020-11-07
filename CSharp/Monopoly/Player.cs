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
    }
}