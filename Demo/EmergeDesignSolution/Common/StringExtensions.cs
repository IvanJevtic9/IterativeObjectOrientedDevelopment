using System;
using System.Collections.Generic;
using System.Linq;

namespace Demo.Common
{
    static class StringExtensions
    {
        public static IEnumerable<IEnumerable<int>> NonNegativeIntegerSequences(this IEnumerable<string> lines) =>
            lines
                .Select(line => line.Split(new[] {' ', '\t'}, StringSplitOptions.RemoveEmptyEntries))
                .Select(segments => segments.Select(segment => (
                    correct: int.TryParse(segment, out int value) && value >= 0,
                    value: value)).ToList())
                .Where(matches => matches.AllNonEmpty(tuple => tuple.correct))
                .Select(matches => matches.Select(tuple => tuple.value));

        public static IEnumerable<int> SingleNonNegativeIntegers(this IEnumerable<string> lines) =>
            lines
                .Select(line => (
                    correct: int.TryParse(line, out int value) && value >= 0,
                    value: value))
                .Where(tuple => tuple.correct)
                .Select(tuple => tuple.value);
    }
}
