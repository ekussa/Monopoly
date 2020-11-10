using System.Collections.Generic;

namespace Monopoly
{
    public interface IPlayerRepository
    {
        Player GetOwnerOf(Property property);
        
        public List<Player> GetAll();
    }
}