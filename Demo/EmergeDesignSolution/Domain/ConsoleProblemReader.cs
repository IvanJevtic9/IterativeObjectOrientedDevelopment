using System;
using System.Linq;
using System.Collections.Generic;
using Demo.EmergeDesignSolution.Common;

namespace Demo.EmergeDesignSolution.Domain
{
    internal class ConsoleProblemReader
    {
        private void PromptInputNumbers() =>
            Console.Write("Input numbers: ");

        private void PromptDesiredResults() =>
            Console.Write("Input desired number: ");

        private IEnumerable<int> DesiredResults =>
            Console.In
                .IncomingLines(PromptDesiredResults)
                .SingleNonNegativeInteger();

        private IEnumerable<IEnumerable<int>> InputNumberSequence =>
            Console.In
                .IncomingLines(PromptInputNumbers)
                .NonNegativeIntegerSequences();
        
        private IEnumerable<(IEnumerable<int> inputs, int desiredNumber)> RawNumbersSequence =>
            InputNumberSequence.Zip(
                DesiredResults,
                (inputs, result) => (inputs, result));

        internal IEnumerable<ProblemStatement> ReadAll() =>
            RawNumbersSequence.Select(tuple => new ProblemStatement(tuple.inputs, tuple.desiredNumber));
    }
}