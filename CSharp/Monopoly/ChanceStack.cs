using System.Collections.Generic;

namespace Monopoly
{
    public class ChanceStack : List<ChanceCard>
    {
        public ChanceStack(IEnumerable<ChanceCard> stack)
        {
            AddRange(stack);
        }
    }
}