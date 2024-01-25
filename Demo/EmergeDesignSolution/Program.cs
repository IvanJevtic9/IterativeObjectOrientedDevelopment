using System;
using System.Linq;
using System.Collections.Generic;
using Demo.EmergeDesignSolution.Domain;
using Demo.EmergeDesignSolution.Common;
using Demo.EmergeDesignSolution.Domain.Expressions;

namespace Demo.EmergeDesignSolution
{
    class Program
    {
        static void Main(string[] args) =>
            ExpressionStreamDemo();

        private static void ExpressionStreamDemo() =>
            InputNumbersSequence
                .Select(new ExpressionStream().DistinctFor)
                .SelectMany(expressions => Report(expressions, "No expression found."))
                .WriteLinesTo(Console.Out);          

        private static void ProductionBehaviour(string[] args) => 
            ProblemStatements.Select(problem => new ExactSolver()
                .DistinctExpressionFor(problem))
                .SelectMany(expressions => Report(expressions, "No solutions for the problem.")) 
                .WriteLinesTo(Console.Out);

        private static IEnumerable<string> Report(IEnumerable<Expression> expressions, string onEmpty) =>
            expressions
                .Select((expression, index) =>
                    $"{index + 1,3}. {expression} = {expression.Value}")
                .DefaultIfEmpty(onEmpty);

        private static IEnumerable<IEnumerable<int>> InputNumbersSequence =>
            new ConsoleInputReader().ReadAll();

        private static IEnumerable<ProblemStatement> ProblemStatements => 
            new ConsoleProblemReader().ReadAll();
    }
}
