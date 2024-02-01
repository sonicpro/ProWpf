using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SampleApplicationModel;

namespace SampleApplicationModelTests
{
    /// <summary>
    /// Tests for CompositeAccount class.
    /// </summary>
    [TestClass]
    public class CompositeAccountTests
    {
        private readonly CompositeAccount testAccount = new CompositeAccount();
        private readonly CompositeAccount childAccount = new CompositeAccount("John");

        [TestInitialize]
        public void OnInitialize()
        {
            var testAccountAccessor = new CompositeAccountAccessor(testAccount);
            var childAccountAccessor = new CompositeAccountAccessor(childAccount);
            childAccountAccessor.Parent = testAccount;
            testAccountAccessor.ChildAccounts = new List<IAccount> { childAccount };
        }

        [TestMethod]
        public void TestAddAccount()
        {
            var account = new CompositeAccount();
            testAccount.AddAccount(account);
            Assert.AreEqual<int>(2, testAccount.ChildAccounts.Count());
            Assert.AreSame(account, testAccount.ChildAccounts.LastOrDefault());
        }

        [TestMethod]
        public void TestRemoveAccount()
        {
            testAccount.RemoveAccount(childAccount);
            Assert.AreEqual<int>(0, testAccount.ChildAccounts.Count);
        }

        [ExpectedException(typeof(InvalidOperationException))]
        [TestMethod]
        public void TestAccountCannotBeMovedWithoutRemovingFirst()
        {
            var newParent = new CompositeAccount();
            var accountWithExistingParent = testAccount.ChildAccounts.First();
            newParent.AddAccount(accountWithExistingParent);
        }

        [ExpectedException(typeof(InvalidOperationException))]
        [TestMethod]
        public void TestAccountNameChouldBeUniqueAmongTheSiblings()
        {
            var sibling = new CompositeAccount("John");
            testAccount.AddAccount(sibling);
        }

        [ExpectedException(typeof(InvalidOperationException))]
        [TestMethod]
        public void TestAccountGraphCannotBeDirectlyCyclical()
        {
            childAccount.AddAccount(testAccount);
        }

        [ExpectedException(typeof(InvalidOperationException))]
        [TestMethod]
        public void TestAccountGraphCannotBeIndirectlyCyclical()
        {
            var childOfAChild = new CompositeAccount();
            childAccount.AddAccount(childOfAChild);
            childOfAChild.AddAccount(testAccount);
        }

        [TestMethod]
        public void TestBalance()
        {
            var leafAccount1 = new LeafAccount("Alex", 10M);
            Entry[] entries = new[]
            {
                new Entry(EntryType.Deposit, 15.25M, string.Empty),
                new Entry(EntryType.Deposit, 16.87M, string.Empty),
                new Entry(EntryType.Withdrawal, 5.12M, string.Empty),
                new Entry(EntryType.Deposit, 8.7M, string.Empty)
            };
            foreach (var e in entries)
            {
                leafAccount1.AddEntry(e);
            }

            var leafAccount2 = new LeafAccount("Shean");
            leafAccount2.AddEntry(new Entry(EntryType.Withdrawal, 5M, string.Empty));
            testAccount.AddAccount(leafAccount1);
            childAccount.AddAccount(leafAccount2);

            Assert.AreEqual(40.7M, testAccount.Balance);
        }
    }
}
