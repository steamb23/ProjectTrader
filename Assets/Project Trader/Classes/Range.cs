using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectTrader
{
    [Serializable]
    public struct IntRange
    {
        public int min;
        public int max;

        public static implicit operator (int min, int max)(IntRange range) => (range.min, range.max);
        public static implicit operator IntRange((int min, int max) tuple) => new IntRange() { min = tuple.min, max = tuple.max };
    }
}
