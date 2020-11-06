using System.Collections.Generic;

namespace Monopoly
{
    public interface IPlayerCursor
    {
        PlayerMove Next();
        List<Player> GetAll();
    }
}