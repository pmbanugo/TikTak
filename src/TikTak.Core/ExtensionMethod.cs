using System.Collections.Generic;
using System.Linq;

namespace TikTak.Core
{
    public static class ExtensionMethod
    {
        public static Dictionary<int, string> ToDictionary(this IEnumerable<KeyValuePair<int, string>> data)
        {
            return data.ToDictionary(x => x.Key, x => x.Value);
        }

        public static IEnumerable<KeyValuePair<int, string>> Where(this IEnumerable<KeyValuePair<int, string>> data, int firstIndex, int secondIndex, int thirdIndex, string value)
        {
            return data.Where(input =>
                (input.Key == firstIndex || input.Key == secondIndex || input.Key == thirdIndex) && input.Value == value);
        }
    }
}
