namespace Demo.Domain.Expressions
{
    class Divide : MultiplicativeExpression
    {
        public Divide(Expression left, Expression right) 
            : base(left, right) { }

        protected override int Combine(int left, int right) =>
            left / right;

        protected override string OperatorToString => "/";
    }
}
