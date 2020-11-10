using System.Collections.Generic;

namespace Monopoly
{
    public interface IPlayerCursor
    {
        IDice Dice { get; set; }
        PlayerMove Next();
        Player GetOwnerOf(Property property);
        List<Player> GetAll();
    }
}