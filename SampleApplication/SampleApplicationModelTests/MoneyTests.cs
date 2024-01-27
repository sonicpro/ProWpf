using System;
using System.Globalization;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SampleApplicationModel;

namespace SampleApplicationModelTests
{
    [TestClass]
    public class MoneyTests
    {
        [TestMethod]
        public void TestMoneyAddition()
        {
            var value1 = new Money(10M);
            var value2 = new Money(5M);
            var result = value1 + value2;
            Assert.AreEqual(new Money(15M), result);
        }

        [TestMethod]
        public void TestMoneyAddition_WithDifferentCurrencies()
        {
            var value1 = new Money(10M, new CultureInfo("en-US"));
            var value2 = new Money(5M, new CultureInfo("en-GB"));
            var result = value1 + value2;
            Assert.AreEqual(Money.Undefined, result);
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void TestMoneyGreaterThan_WithDifferentCurrencies()
        {
            var value1 = new Money(10M, new CultureInfo("en-US"));
            var value2 = new Money(5M, new CultureInfo("en-GB"));
            var _ = value1 > value2;
        }
    }
}