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

        private static Expression Solve(ProblemStatement problem)
        {
            var combining = new Queue<Expression>(problem
                .InputNumbers
                .Select(number => new Expression(number)));

            var known = new List<Expression>();
            while (combining.TryDequeue(out Expression current))
            {
                if(current.Value == problem.DesiredResult)
                {
                    return current;
                }

                var combinableWith = known
                    .Where(expr =>
                        !expr.UsedNumbers
                            .Intersect(current.UsedNumbers)
                            .Any());

                foreach (var existing in combinableWith)
                {
                    combining.Enqueue(
                        current.CombineWith(existing, '+', current.Value + existing.Value));
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
