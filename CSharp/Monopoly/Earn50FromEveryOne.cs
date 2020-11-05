using System.Collections.Generic;

namespace Monopoly
{
    public class Earn50FromEveryOne : ChanceCard
    {
        public List<Player> Players { get; }
        
        public Earn50FromEveryOne(string text, List<Player> players) : base(text)
        {
            Text = text;
            Players = players;
        }
    }
}