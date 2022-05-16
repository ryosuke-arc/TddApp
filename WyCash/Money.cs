namespace WyCash
{
    public class Money : IExpression
    {
        internal int amount;

        internal string currency;


        public Money(int amount, string currency)
        {
            this.amount = amount;
            this.currency = currency;
        }
        public IExpression Times(int multiplier)
        {
            return new Money(amount * multiplier, this.currency);
        }

        public IExpression Plus(IExpression addend)
        {
            return new Sum(this, addend);
        }

        public Money Reduce(Bank bank, string to)
        {
            int rate = bank.Rate(this.currency, to);
            return new Money(this.amount / rate, to);
        }

        public string Currency()
        {
            return this.currency;
        }

        public override bool Equals(object obj)
        {
            var money = (Money)obj;
            return amount == money.amount &&
                this.Currency().Equals(money.Currency());
        }

        public static Money Doller(int amount)
        {
            return new Money(amount, "USD");
        }

        public static Money Franc(int amount)
        {
            return new Money(amount, "CHF");
        }

        public override string ToString()
        {
            return $"{this.amount}{currency}";
        }

        public override int GetHashCode()
        {
            throw new System.NotImplementedException();
        }
    }
}
