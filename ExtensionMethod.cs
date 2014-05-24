using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TikTak
{
    public static class ExtensionMethod
    {
        public static Dictionary<int, string> ToDictionary(this IEnumerable<KeyValuePair<int, string>> data)
        {
            return data.ToDictionary(x => x.Key, x => x.Value);
        }
    }
}
