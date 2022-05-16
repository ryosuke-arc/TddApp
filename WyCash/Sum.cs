namespace WyCash
{
    public class Sum : IExpression
    {
        public IExpression augend;

        public IExpression addend;

        public Sum(IExpression augend, IExpression addend)
        {
            this.augend = augend;
            this.addend = addend;
        }

        public IExpression Times(int multiplier)
        {
            return new Sum(
                this.augend.Times(multiplier),
                this.addend.Times(multiplier));
        }

        public IExpression Plus(IExpression addend)
        {
            return new Sum(this, addend);
        }

        public Money Reduce(Bank bank, string to)
        {
            var amount = augend.Reduce(bank, to).amount
                + addend.Reduce(bank, to).amount;
            return new Money(amount, to);
        }
    }
}
