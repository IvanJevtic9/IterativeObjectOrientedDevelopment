using System;
using System.Linq;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace Demo
{
    /*
        1 2 3 4 - 3 => 1 + 2 , 6 8 2 - 24 => 6 * 8 / 2 , (+ - / *)
    */
    class Program
    {
        static void Main(string[] args)
        {
            while (true)
            {
                var problem = ReadProblem();
                if (problem is null)
                    break;

                var solution = Solve(problem);
                var report = solution?.ToString() ?? "No solution found";
                Console.WriteLine(report);
                Console.WriteLine();
            }
        }

        private static IEnumerable<int> ExceptWithDuplicates(IEnumerable<int> from, IEnumerable<int> remove) =>
            from.Select(value => (value: value, count: 1))
                .Concat(remove.Select(value => (value: value, count: -1)))
                .GroupBy(tuple => tuple.value)
                .Select(group => (value: group.Key, count: group.Sum(tuple => tuple.count)))
                .Where(tuple => tuple.count > 0)
                .SelectMany(tuple => Enumerable.Repeat(tuple.value, tuple.count));

        private static bool IsSuperset(IEnumerable<int> sequence, IEnumerable<int> of) =>
            sequence.Select(value => (value: value, count: 1))
                    .Concat(of.Select(value => (value: value, count: -1)))
                    .GroupBy(tuple => tuple.value)
                    .All(group => group.Sum(tuple => tuple.count) >= 0);

        private static Expression Solve(ProblemStatement problem)
        {
            var combining = new Queue<Expression>(problem
                .InputNumbers
                .Select(number => new Expression(number)));

            var known = new HashSet<Expression>();
            while (combining.TryDequeue(out Expression current))
            {
                if(current.Value == problem.DesiredResult)
                {
                    return current;
                }

                var availableNumbers = ExceptWithDuplicates(problem.InputNumbers, current.UsedNumbers);

                var combinableWith = known
                    .Where(expr => 
                        IsSuperset(availableNumbers, expr.UsedNumbers));

                foreach (var existing in combinableWith)
                {
                    combining.Enqueue(current
                        .CombineWith(existing, '+', current.Value + existing.Value));

                    combining.Enqueue(current
                        .CombineWith(existing, '*', current.Value * existing.Value));

                    combining.Enqueue(current
                        .CombineWith(existing, '-', current.Value - existing.Value));

                    combining.Enqueue(current
                        .CombineWith(existing, '-', existing.Value - current.Value));

                    if(existing.Value != 0 && current.Value % existing.Value == 0)
                    {
                        combining.Enqueue(current
                            .CombineWith(existing, '/', current.Value / existing.Value));
                    }

                    if (current.Value != 0 && existing.Value % current.Value == 0)
                    {
                        combining.Enqueue(current
                            .CombineWith(existing, '/', existing.Value / current.Value));
                    }
                }

                known.Add(current);
            }

            return null;
        }

        private static ProblemStatement ReadProblem()
        {
            Console.Write("Numbers to user (RETURN to exit): ");
            var line = Console.ReadLine() ?? string.Empty;

            var values = line.Split(
                new[] { " ", "\t" },
                StringSplitOptions.RemoveEmptyEntries);

            var input = values
                .Where(value => Regex.IsMatch(value, @"^\d+$"))
                .Select(int.Parse)
                .ToList();

            if (input.Count == 0)
            {
                return null;
            }

            Console.Write("            Enter desired result: ");
            var rawNumber = Console.ReadLine() ?? string.Empty;

            if (!int.TryParse(rawNumber, out var desiredNumber))
            {
                desiredNumber = 0;
            }

            return new ProblemStatement(input, desiredNumber);
        }
    }
}
