using Microsoft.VisualStudio.TestTools.UnitTesting;
using SampleApplicationModel;
using Entry = SampleApplicationModel.Entry;

namespace SampleApplicationModelTests
{
    [TestClass]
    public class LeafAccountTests
    {
        [TestMethod]
        public void TestBalance()
        {
            var account = new LeafAccount("John", Money.Zero);
            Entry[] entries = new[]
            {
                new Entry(EntryType.Deposit, 15.25M, string.Empty),
                new Entry(EntryType.Deposit, 16.87M, string.Empty),
                new Entry(EntryType.Withdrawal, 5.12M, string.Empty),
                new Entry(EntryType.Deposit, 8.7M, string.Empty)
            };
            foreach (var e in entries)
            {
                account.AddEntry(e);
            }

            Assert.AreEqual(new Money(35.7M), account.Balance);
        }
    }
}
