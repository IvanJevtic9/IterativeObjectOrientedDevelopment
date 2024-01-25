using System.IO;
using System.Linq;
using System.Collections.Generic;
using System;

namespace Demo.EmergeDesignSolution.Common
{
    internal static class EnumerableExtensions
    {
        public static void WriteLinesTo<T>(this IEnumerable<T> sequence, TextWriter destination) =>
            sequence.Select(seq => $"{seq}")
                    .WriteLinesTo(destination);

        public static void WriteLinesTo(this IEnumerable<string> sequence, TextWriter destination)
        {
            foreach (string line in sequence)
            {
                destination.WriteLine(line);
            }
        }

        public static bool AllNonEmpty<T>(this IEnumerable<T> sequence, Func<T, bool> predicate)
        {
            bool any = false;

            foreach (T item in sequence)
            {
                if (!predicate(item))
                {
                    return false;
                }

                any = true;
            }

            return any;
        }

        public static bool IsEmpty<T>(this IEnumerable<T> sequence) => !sequence.Any();
    }
}
