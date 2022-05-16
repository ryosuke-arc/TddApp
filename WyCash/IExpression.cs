namespace WyCash
{
    public interface IExpression
    {
        public IExpression Plus(IExpression addend);

        public IExpression Times(int multiplier);

        public Money Reduce(Bank bank, string to);
    }
}
