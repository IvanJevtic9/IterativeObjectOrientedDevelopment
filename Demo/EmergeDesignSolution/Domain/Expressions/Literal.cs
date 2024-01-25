namespace Demo.Domain.Expressions
{
    class Literal : Expression
    {
        public override int Value { get; }

        public Literal(int value)
        {
            this.Value = value;
        }

        public override string ToString() => 
            $"{this.Value}";
    }
}