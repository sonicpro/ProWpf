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
    }
}
