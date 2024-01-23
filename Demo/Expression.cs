using System;
using System.Collections.Generic;
using System.Linq;

namespace Demo
{
    internal class Expression
    {
        private char Operator { get; }
        private Expression LeftChild { get; }
        private Expression RightChild { get; }

        public int Value { get; }
        public IEnumerable<int> UsedNumbers { get; }

        public Expression(int value)
        {
            Operator = '\0';

            Value = value;
            UsedNumbers = new[] { value };
        }

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
            UsedNumbers = leftChild.UsedNumbers.Union(rightChild.UsedNumbers);
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
    }
}