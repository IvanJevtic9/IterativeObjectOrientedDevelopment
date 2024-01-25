namespace Demo.Domain.Expressions
{
    abstract class MultiplicativeExpression : BinaryExpression
    {
        protected MultiplicativeExpression(Expression left, Expression right) 
            : base(left, right) { }

        public override string ToString() =>
            $"{this.Parenthesize(base.Left)} {this.OperatorToString} {this.Parenthesize(base.Right)}";

        private string Parenthesize(Expression child) =>
            this.IsAdditive(child) ? $"({child})" : $"{child}";

        private bool IsAdditive(Expression child) =>
            child is Add || child is Subtract;
    }
}
