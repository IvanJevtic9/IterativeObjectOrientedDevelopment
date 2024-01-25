using System;
using System.Collections.Generic;
using System.Linq;
using Demo.Common;
using Demo.Domain;

namespace Demo
{
    class Program
    {
        static void Main(string[] args) =>
            ExpressionsStreamDemo();

        static void PartitioningDemo() =>
            InputNumberSequences
                .SelectMany(numbers => Partitionings.Of(numbers).All())
                .Select(partitioning => partitioning.Select(partition => string.Join(" ", partition.ToArray())))
                .Select(partitions => string.Join(" | ", partitions))
                .WriteLinesTo(Console.Out);

        static void ExpressionsStreamDemo() =>
            InputNumberSequences
                .Select(new ExpressionStream().DistinctFor)
                .SelectMany(expressions => Report(expressions, "No expressions found."))
                .WriteLinesTo(Console.Out);

        static void ProductionBehavior() =>
            ProblemStatements
                .Select(problem => new ExactSolver().DistinctExpressionsFor(problem))
                .SelectMany(expressions => Report(expressions, "No solutions found for the problem."))
                .WriteLinesTo(Console.Out);

        static IEnumerable<string> Report(IEnumerable<Expression> expressions, string onEmpty) =>
            expressions
                .Select((expression, index) => $"{index + 1,3}. {expression} = {expression.Value}")
                .DefaultIfEmpty(onEmpty);

        private static IEnumerable<ProblemStatement> ProblemStatements =>
            new ConsoleProblemsReader().ReadAll();

        private static IEnumerable<IEnumerable<int>> InputNumberSequences =>
            new ConsoleInputsReader().ReadAll();
    }
}