using System;
using System.Collections.Generic;

namespace Demo.Common
{
    static class ObjectExtensions
    {
        public static IEnumerable<T> ExpandEndlessly<T>(this T target, Func<T, IEnumerable<T>> expansion)
        {
            Queue<T> toExpand = new Queue<T>();
            toExpand.Enqueue(target);

            while (toExpand.TryDequeue(out T current))
            {
                yield return current;
                toExpand.EnqueueMany(expansion(current));
            }
        }
    }
}