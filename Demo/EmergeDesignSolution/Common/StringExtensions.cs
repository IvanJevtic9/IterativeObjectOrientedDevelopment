using System.Linq;
using System.Collections.Generic;

namespace Demo.EmergeDesignSolution.Common
{
    public static class StringExtensions
    {
        public static IEnumerable<IEnumerable<int>> NonNegativeIntegerSequences(this IEnumerable<string> lines) =>
            lines.Select(line => line.Split(
                    new[] { ' ', '\t' },
                    System.StringSplitOptions.RemoveEmptyEntries))
                .Select(stretches =>
                    stretches.Select(stretch =>
                        (correct: int.TryParse(stretch, out int value) && value >= 0, value: value))
                            .ToList())
                .Where(matches => matches.AllNonEmpty(tuple => tuple.correct))
                .Select(matches => matches.Select(tuple => tuple.value));

        public static IEnumerable<int> SingleNonNegativeInteger(this IEnumerable<string> lines) =>
            lines.Select(line =>
                    (correct: int.TryParse(line, out int value) && value >= 0, value: value))
                .Where(tuple => tuple.correct)
                .Select(tuple => tuple.value);
    }
}
