using System.Collections.Generic;

namespace WyCash
{
    public class Bank
    {
        private Dictionary<Pair, int> rates = new Dictionary<Pair, int>();

        public Money Reduce(IExpression source, string to)
        {
            return source.Reduce(this, to);
        }

        public void AddRate(string from, string to, int rate)
        {
            this.rates.Add(new Pair(from, to), rate);
        }

        public int Rate(string from, string to)
        {
            if (from.Equals(to)) return 1;
            return rates.GetValueOrDefault(new Pair(from, to));
        }
    }
}
