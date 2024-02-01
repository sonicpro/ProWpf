using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using SampleApplicationModel;

namespace SampleApplicationModelTests
{
    [TestClass]
    public class EntryTests
    {
        [TestMethod]
        public void TestApplyDepositEntry()
        {
            var balance = new Money(10M);
            var entry = new Entry(EntryType.Deposit, 5M, string.Empty);
            Assert.AreEqual(15M, entry.ApplyEntry(balance));
        }

        [TestMethod]
        public void TestApplyWithdrawalEntry()
        {
            var balance = new Money(10M);
            var entry = new Entry(EntryType.Withdrawal, 5M, string.Empty);
            Assert.AreEqual(5M, entry.ApplyEntry(balance));
        }
    }
}
