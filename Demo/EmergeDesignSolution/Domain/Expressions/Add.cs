namespace Demo.EmergeDesignSolution.Domain.Expressions
{
    internal class Add : Expression
    {
        private Expression Left { get; }
        private Expression Right { get; }

        public override int Value => Left.Value + Right.Value;

        public Add(Expression left, Expression right) =>
            (Left, Right) = (left, right);

        public override string ToString() =>
            $"{Left} + {Right}";
    }
}