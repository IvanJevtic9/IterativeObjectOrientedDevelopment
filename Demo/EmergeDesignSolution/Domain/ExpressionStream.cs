using System;
using System.Collections.Generic;
using System.Linq;
using Demo.Common;
using Demo.Domain.Expressions;

namespace Demo.Domain
{
    class ExpressionStream
    {
        public IEnumerable<Expression> DistinctFor(IEnumerable<int> inputNumbers) =>
            this.DistinctFor(this.AsLiterals(inputNumbers), 1, this.MultiplyAndDivide, this.CreateAdditive);

        private IEnumerable<Expression> DistinctFor(
            IEnumerable<Expression> elements,
            int minPartitions,
            Func<Partition<Expression>, IEnumerable<Expression>> partitionExpressionBuilder,
            Func<Expression, IEnumerable<Expression>, IEnumerable<Expression>, IEnumerable<Expression>> reduceExpressionBuilder) =>
            elements.Take(2).Count() == 1 ? 
            elements : 
            Partitionings.Of(elements)
                .All()
                .Where(partitioning => partitioning.Count() >= minPartitions)
                .SelectMany(partitioning => partitioning.Select(partitionExpressionBuilder).CrossProduct())
                .SelectMany(subexpressions =>
                    this.ThreeWaySplit(subexpressions)
                        .SelectMany(split => reduceExpressionBuilder(split.head, split.direct, split.inverse)));

        private IEnumerable<(Expression head, Partition<Expression> direct, Partition<Expression> inverse)>
            ThreeWaySplit(IEnumerable<Expression> expressions) =>
            expressions
                .AsPartition()
                .Split()
                .Where(split => split.left.Any())
                .Select(split => (split.left.First(), split.left.Skip(1).AsPartition(), split.right));

        private IEnumerable<Expression> AsLiterals(IEnumerable<int> inputNumbers) =>
            inputNumbers.Select(number => new Literal(number));

        private IEnumerable<Expression> AddAndSubtract(IEnumerable<Expression> expressions) =>
            this.DistinctFor(expressions, 2, this.MultiplyAndDivide, this.CreateAdditive);

        private IEnumerable<Expression> MultiplyAndDivide(IEnumerable<Expression> expressions) =>
            this.DistinctFor(expressions, 2, this.AddAndSubtract, this.CreateMultiplicative);

        private IEnumerable<Expression> CreateAdditive(
            Expression head, IEnumerable<Expression> add, IEnumerable<Expression> subtract) =>
            head.Add(add).TrySubtract(subtract);

        private IEnumerable<Expression> CreateMultiplicative(
            Expression head, IEnumerable<Expression> multiply, IEnumerable<Expression> divide) =>
            head.Multiply(multiply).TryDivide(divide);
    }
}