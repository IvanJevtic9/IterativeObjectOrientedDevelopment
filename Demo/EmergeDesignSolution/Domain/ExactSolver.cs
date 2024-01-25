using System.Collections.Generic;
using System.Linq;

namespace Demo.Domain
{
    class ExactSolver
    {
        public IEnumerable<Expression> DistinctExpressionsFor(ProblemStatement problem) =>
            new ExpressionStream()
                .DistinctFor(problem.InputNumbers)
                .Where(expression => expression.Value == problem.DesiredResult);
    }
}