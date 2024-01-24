using System;
using System.Linq;
using System.Collections.Generic;

namespace Demo.FirstIterativeSolution
{
    internal class Expression : IEquatable<Expression>
    {
        private char Operator { get; }
        private Expression LeftChild { get; }
        private Expression RightChild { get; }

        public int Value { get; }
        public IEnumerable<int> UsedNumbers { get; }

        public Expression(int value) =>
            (Operator, Value, UsedNumbers) = ('\0', value, new[] { value });

        public Expression(
            int value,
            char @operator,
            Expression leftChild,
            Expression rightChild)
        {
            Operator = @operator;
            LeftChild = leftChild;
            RightChild = rightChild;

            Value = value;
            UsedNumbers = leftChild.UsedNumbers.Concat(rightChild.UsedNumbers);
        }
        internal Expression CombineWith(Expression other, char @operator, int value) =>
            new Expression(value, @operator, this, other);

        public override string ToString() =>
            $"{PlainToString(this)} = {Value}";

        private string Parenthesize(Expression child) =>
            child.Operator == '\0' ? $"{child.Value}" : $"({PlainToString(child)})";

        private string PlainToString(Expression expr) =>
            expr.Operator == '\0' ?
                $"{expr.Value}" :
                $"{expr.Parenthesize(expr.LeftChild)} {Operator} {expr.Parenthesize(expr.RightChild)}";

        public override bool Equals(object other) => Equals(other as Expression);

        public bool Equals(Expression other) =>
            !(other is null) &&
            Value == other.Value &&
            Operator == other.Operator &&
            NullableEqual(LeftChild, other.LeftChild) &&
            NullableEqual(RightChild, other.RightChild);

        private bool NullableEqual(Expression a, Expression b) =>
            a is null && b is null || !(a is null) && a.Equals(b);

        public override int GetHashCode() =>
            Operator.GetHashCode() ^
            Value << 1 ^
            (LeftChild?.GetHashCode() ?? 0) << 2 ^
            (RightChild?.GetHashCode() ?? 0) << 3;
    }
}