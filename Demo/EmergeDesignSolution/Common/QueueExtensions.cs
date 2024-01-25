using System.Collections.Generic;

namespace Demo.Common
{
    static class QueueExtensions
    {
        public static void EnqueueMany<T>(this Queue<T> target, IEnumerable<T> objects)
        {
            foreach (T obj in objects)
                target.Enqueue(obj);
        }
    }
}