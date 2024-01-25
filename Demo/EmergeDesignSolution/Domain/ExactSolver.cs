using System.Linq;
using System.Collections.Generic;
using Demo.EmergeDesignSolution.Domain.Expressions;

namespace Demo.EmergeDesignSolution.Domain
{
    internal class ExactSolver
    {
        public IEnumerable<Expression> DistinctExpressionFor(ProblemStatement problem) =>
            new ExpressionStream()
                .DistinctFor(problem.InputNumbers)
                .Where(expression => expression.Value == problem.DesiredNumber);
    }
}