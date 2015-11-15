using System;
using System.Collections.Generic;

namespace CountingKs.Extensions
{
    public static class LinqExtensions
    {
        public static void ForEach<T>(this IEnumerable<T> source, Action<T> action)
        {
            if (source == null)
                throw new ArgumentNullException(nameof(source));

            foreach (var item in source)
                action(item);
        }
    }
}