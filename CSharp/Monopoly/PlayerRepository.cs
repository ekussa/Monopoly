using System.Collections.Generic;
using System.Linq;

namespace Monopoly
{
    public class PlayerRepository : IPlayerRepository
    {
        private readonly List<Player> _players;

        public PlayerRepository(List<Player> players)
        {
            _players = players;
        }
        
        public Player GetOwnerOf(Property property)
        {
            return
                _players.
                    FirstOrDefault(player =>
                        player.Patrimony.Owns(property));
        }

        public List<Player> GetAll()
        {
            return _players;
        }
    }
}