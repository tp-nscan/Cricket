using System;
using System.Collections.Generic;

namespace Cricket.Common
{
    public static class EnumerExt
    {
        public static void ForEach<T>(this IEnumerable<T> list, Action<T> action)
        {
            foreach (var pair in list)
            {
                action.Invoke(pair);
            }
        }
    }
}
