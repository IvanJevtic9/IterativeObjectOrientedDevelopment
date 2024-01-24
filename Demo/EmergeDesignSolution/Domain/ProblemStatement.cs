using System.Linq;
using System.Collections.Generic;

namespace Demo.EmergeDesignSolution.Domain
{
    internal class ProblemStatement
    {
        private int DesiredNumber;
        private IEnumerable<int> InputNumbers;

        public ProblemStatement(IEnumerable<int> inputs, int desiredNumber)
        {
            InputNumbers = inputs;
            DesiredNumber = desiredNumber;
        }

        private string InputNumberFormattedList =>
            string.Join(", ", InputNumbers.Select(number => $"{number}").ToArray());

        public override string ToString() =>
            $"Problem statement: [{InputNumberFormattedList}] -> {DesiredNumber}";
    }
}