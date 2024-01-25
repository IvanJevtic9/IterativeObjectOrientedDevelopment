using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Demo.Common
{
    static class EnumerableExtensions
    {
        public static void WriteLinesTo<T>(this IEnumerable<T> sequence, TextWriter destination) =>
            sequence.Select(obj => $"{obj}").WriteLinesTo(destination);

        public static void WriteLinesTo(this IEnumerable<string> lines, TextWriter destination)
        {
            foreach (string line in lines)
                destination.WriteLine(line);
        }

        public static bool AllNonEmpty<T>(this IEnumerable<T> sequence, Func<T, bool> predicate)
        {
            bool any = false;

            foreach (T obj in sequence)
            {
                if (!predicate(obj)) return false;
                any = true;
            }

            return any;
        }

        public static bool IsEmpty<T>(this IEnumerable<T> sequence) => !sequence.Any();

        public static Partition<T> AsPartition<T>(this IEnumerable<T> sequence) =>
            new Partition<T>(sequence);

        public static (IEnumerable<T> prefix, T last) ExtractLast<T>(this IEnumerable<T> sequence)
        {
            List<T> prefix = new List<T>();
            using (IEnumerator<T> enumerator = sequence.GetEnumerator())
            {
                enumerator.MoveNext();
                T last = enumerator.Current;

                while (enumerator.MoveNext())
                {
                    prefix.Add(last);
                    last = enumerator.Current;
                }

                return (prefix, last);
            }
        }

        public static IEnumerable<IEnumerable<T>> CrossProduct<T>(this IEnumerable<IEnumerable<T>> sequenceOfSequences)
        {
            T[][] data = sequenceOfSequences.Select(sequence => sequence.ToArray()).ToArray();
            int[] indices = new int[data.Length];
            int carryOver = 0;

            while (carryOver == 0)
            {
                yield return indices.Select((column, row) => data[row][column]).ToList();
                carryOver = 1;
                for (int row = 0; carryOver > 0 && row < indices.Length; row++)
                {
                    indices[row] += 1;
                    carryOver = indices[row] / data[row].Length;
                    indices[row] = indices[row] % data[row].Length;
                }
            }
        }
    }
}
