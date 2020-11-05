using System;
using System.Collections.Generic;
using System.Linq;

namespace Monopoly
{
    public static class Extensions
    {
        public static IEnumerable<T> AsRandom<T>(this IList<T> list)
        {
            var indexes = Enumerable.Range(0, list.Count).ToArray();
            var generator = new Random();
    
            for (var i = 0; i < list.Count; ++i )
            {
                var position = generator.Next(i, list.Count);
                yield return list[indexes[position]];
                indexes[position] = indexes[i];
            }
        }
    }
}