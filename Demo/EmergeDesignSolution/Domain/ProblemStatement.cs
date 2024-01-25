using System.Linq;
using System.Collections.Generic;

namespace Demo.EmergeDesignSolution.Domain
{
    internal class ProblemStatement
    {
        public IEnumerable<int> InputNumbers;
        public int DesiredNumber { get; }

        public ProblemStatement(IEnumerable<int> inputs, int desiredNumber)
        {
            InputNumbers = inputs ?? Enumerable.Empty<int>();
            DesiredNumber = desiredNumber;
        }

        private string InputNumberFormattedList =>
            string.Join(", ", InputNumbers.Select(number => $"{number}").ToArray());

        public override string ToString() =>
            $"Problem statement: [{InputNumberFormattedList}] -> {DesiredNumber}";
    }
}