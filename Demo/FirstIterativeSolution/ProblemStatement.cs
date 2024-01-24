using System.Collections.Generic;

namespace Demo.FirstIterativeSolution
{
    internal class ProblemStatement
    {
        public int DesiredResult { get; }
        public IEnumerable<int> InputNumbers { get; }

        public ProblemStatement(IEnumerable<int> inputNumbers, int desiredResult) =>
            (DesiredResult, InputNumbers) = (desiredResult, inputNumbers);
    }
}