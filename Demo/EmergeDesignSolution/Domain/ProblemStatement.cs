using System.Collections.Generic;
using System.Linq;

namespace Demo.Domain
{
    class ProblemStatement
    {
        public IEnumerable<int> InputNumbers { get; }
        public int DesiredResult { get; }

        public ProblemStatement(IEnumerable<int> inputNumbers, int desiredResult)
        {
            this.InputNumbers = inputNumbers;
            this.DesiredResult = desiredResult;
        }

        public override string ToString() =>
            $"Problem statement: {{{this.InputNumbersFormattedList}}} -> {this.DesiredResult}";

        private string InputNumbersFormattedList =>
            string.Join(", ", this.InputNumbers.Select(number => $"{number}").ToArray());
    }
}