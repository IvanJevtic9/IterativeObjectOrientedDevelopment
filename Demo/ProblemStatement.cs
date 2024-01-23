using System.Collections.Generic;

namespace Demo
{
    internal class ProblemStatement
    {
        public int DesiredResult { get; }
        public IEnumerable<int> InputNumbers { get; }
        
        public ProblemStatement(IEnumerable<int> inputNumbers, int desiredResult) =>
            (DesiredResult, InputNumbers) = (desiredResult, inputNumbers);
    }
}