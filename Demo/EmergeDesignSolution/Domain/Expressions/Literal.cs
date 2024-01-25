namespace Demo.EmergeDesignSolution.Domain.Expressions
{
    internal class Literal : Expression
    {
        public override int Value { get; }

        public Literal(int value) => Value = value;

        public override string ToString() => $"{Value}";
    }
}