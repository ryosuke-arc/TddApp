using NUnit.Framework;
using WyCash;

namespace WyCashTest
{
    public class MoneyTest
    {
        [Test]
        public void TestMoneyMultiplication()
        {
            Money five = Money.Doller(5);
            Assert.AreEqual(Money.Doller(10), five.Times(2));
            Assert.AreEqual(Money.Doller(15), five.Times(3));
        }

        [Test]
        public void TestEquality()
        {
            Assert.IsTrue(Money.Doller(5).Equals(Money.Doller(5)));
            Assert.IsFalse(Money.Doller(5).Equals(Money.Doller(6)));
            Assert.IsFalse(Money.Franc(5).Equals(Money.Doller(5)));
        }

        [Test]
        public void TestCurrency()
        {
            Assert.AreEqual("USD", Money.Doller(1).Currency());
            Assert.AreEqual("CHF", Money.Franc(1).Currency());
        }

        [Test]
        public void TestSimpleAddition()
        {
            var five = Money.Doller(5);
            var sum = five.Plus(five);
            var bank = new Bank();
            var reduced = bank.Reduce(sum, "USD");
            Assert.AreEqual(Money.Doller(10), reduced);
        }

        [Test]
        public void TestPlusReturnsSum()
        {
            var five = Money.Doller(5);
            var result = five.Plus(five);
            var sum = (Sum)result;
            Assert.AreEqual(five, sum.augend);
            Assert.AreEqual(five, sum.addend);
        }

        [Test]
        public void TestReduceSum()
        {
            var sum = new Sum(Money.Doller(3), Money.Doller(4));
            var bank = new Bank();
            var result = bank.Reduce(sum, "USD");
            Assert.AreEqual(Money.Doller(7), result);
        }

        [Test]
        public void TestReduceMoney()
        {
            var bank = new Bank();
            var result = bank.Reduce(Money.Doller(1), "USD");
            Assert.AreEqual(Money.Doller(1), result);
        }

        [Test]
        public void TestReduceMoneyDifferentCurrency()
        {
            var bank = new Bank();
            bank.AddRate("CHF", "USD", 2);
            var result = bank.Reduce(Money.Franc(2), "USD");
            Assert.AreEqual(Money.Doller(1), result);
        }

        [Test]
        public void TestIdentityRate()
        {
            Assert.AreEqual(1, new Bank().Rate("USD", "USD"));
        }

        [Test]
        public void TestMixedAddition()
        {
            var fiveBucks = Money.Doller(5);
            var tenFracns = Money.Franc(10);
            var bank = new Bank();
            bank.AddRate("CHF", "USD", 2);
            var result = bank.Reduce(fiveBucks.Plus(tenFracns), "USD");
            Assert.AreEqual(Money.Doller(10), result);
        }

        [Test]
        public void TestSumMoney()
        {
            var fiveBacks = Money.Doller(5);
            var tenFrancs = Money.Franc(10);
            var bank = new Bank();
            bank.AddRate("CHF", "USD", 2);
            var sum = new Sum(fiveBacks, tenFrancs).Plus(fiveBacks);
            var result = bank.Reduce(sum, "USD");
            Assert.AreEqual(Money.Doller(15), result);
        }

        [Test]
        public void TestSumTimes()
        {
            var fiveBacks = Money.Doller(5);
            var tenFrancs = Money.Franc(10);
            var bank = new Bank();
            bank.AddRate("CHF", "USD", 2);
            var sum = new Sum(fiveBacks, tenFrancs).Times(2);
            var result = bank.Reduce(sum, "USD");
            Assert.AreEqual(Money.Doller(20), result);
        }
    }
}
