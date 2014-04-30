using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TikTak
{
    public static class Utility
    {
        static private int[,] winners = new int[,]
				   {
						{0,1,2},
						{3,4,5},
						{6,7,8},
						{0,3,6},
						{1,4,7},
						{2,5,8},
						{0,4,8},
						{2,4,6}
				   };
        public static int[,] Winners { get { return winners; } }

        public static Dictionary<int, string> ToDictionary(this IEnumerable<KeyValuePair<int, string>> data)
        {
            return data.ToDictionary(x => x.Key, x => x.Value);
        }
    }
}
