using System;
using System.Linq;
using System.Collections.Generic;
using Demo.EmergeDesignSolution.Common;

namespace Demo.EmergeDesignSolution.Domain
{
    internal class ConsoleInputReader
    {
        private string Prompt { get; } = string.Empty;

        public ConsoleInputReader() : this("Input numbers: ") { }

        public ConsoleInputReader(string prompt) =>
            Prompt = prompt ?? string.Empty;

        private void PromptInputNumbers() =>
            Console.Write(Prompt);

        internal IEnumerable<IEnumerable<int>> ReadAll() =>
            Console.In
                .IncomingLines(PromptInputNumbers)
                .NonNegativeIntegerSequences();
    }

    internal class ConsoleProblemReader
    {
        private void PromptDesiredResults() =>
            Console.Write("Input desired number: ");

        private IEnumerable<int> DesiredResults =>
            Console.In
                .IncomingLines(PromptDesiredResults)
                .SingleNonNegativeInteger();

        private IEnumerable<IEnumerable<int>> InputNumberSequence =>
            new ConsoleInputReader("Input numbers: ").ReadAll();

        private IEnumerable<(IEnumerable<int> inputs, int desiredNumber)> RawNumbersSequence =>
            InputNumberSequence.Zip(
                DesiredResults,
                (inputs, result) => (inputs, result));

        internal IEnumerable<ProblemStatement> ReadAll() =>
            RawNumbersSequence.Select(tuple => new ProblemStatement(tuple.inputs, tuple.desiredNumber));
    }
}