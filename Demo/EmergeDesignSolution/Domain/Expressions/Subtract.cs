namespace Demo.Domain.Expressions
{
    class Subtract : BinaryExpression
    {
        public Subtract(Expression left, Expression right) 
            : base(left, right) { }

        protected override int Combine(int left, int right) =>
            left - right;

        protected override string OperatorToString => "-";
    }
}