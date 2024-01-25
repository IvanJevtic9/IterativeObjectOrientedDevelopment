using System;
using System.Collections.Generic;
using System.Linq;
using Demo.Common;

namespace Demo.Domain
{
    class ConsoleProblemsReader
    {
        private ConsoleInputsReader InputsReader { get; } =
            new ConsoleInputsReader(" Input numbers: ");

        public IEnumerable<ProblemStatement> ReadAll() =>
            this.RawNumbersSequence.Select(tuple => new ProblemStatement(tuple.inputs, tuple.result));

        private IEnumerable<(IEnumerable<int> inputs, int result)> RawNumbersSequence =>
            this.InputNumberSequences.Zip(this.DesiredResults, (inputs, result) => (inputs, result));

        private IEnumerable<IEnumerable<int>> InputNumberSequences =>
            this.InputsReader.ReadAll();

        private IEnumerable<int> DesiredResults =>
            Console.In.IncomingLines(this.PromptDesiredResult).SingleNonNegativeIntegers();
        
        private void PromptDesiredResult() =>
            Console.Write("Desired result: ");
    }
}