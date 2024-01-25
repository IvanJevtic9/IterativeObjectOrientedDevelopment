using System.Linq;
using System.Collections.Generic;
using Demo.EmergeDesignSolution.Common;
using Demo.EmergeDesignSolution.Domain.Expressions;

namespace Demo.EmergeDesignSolution.Domain
{
    internal class ExpressionStream
    {
        public IEnumerable<Expression> DistinctFor(IEnumerable<int> inputNumbers) =>
            inputNumbers.IsEmpty() ? Enumerable.Empty<Expression>() : [Add(inputNumbers)];

        private Expression Add(IEnumerable<int> numbers) =>
            numbers.Select<int, Expression>(number => new Literal(number))
                   .Aggregate((left, next) => new Add(left, next));
    }
}