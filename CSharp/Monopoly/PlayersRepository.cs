using System.Drawing;
using System.Linq;

namespace Monopoly
{
    public class PlayersRepository
    {
        public Player[] Players { get; }
        
        public PlayersRepository(Color[] colors)
        {
            //Players = colors.Select(color => new Player(color)).ToArray();
        }
    }
}