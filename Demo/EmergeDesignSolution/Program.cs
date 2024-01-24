using System;
using System.Collections.Generic;
using Demo.EmergeDesignSolution.Common;
using Demo.EmergeDesignSolution.Domain;

namespace Demo.EmergeDesignSolution
{
    class Program
    {
        private static IEnumerable<ProblemStatement> ProblemStatements => new ConsoleProblemReader().ReadAll();

        static void Main(string[] args)
        {
            ProblemStatements.WriteLinesTo(Console.Out);
        }
    }
}
