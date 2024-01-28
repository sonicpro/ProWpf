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
        private readonly IAccount childAccount = new CompositeAccount();

        [TestInitialize]
        public void OnInitialize()
        {
            var testAccountAccessor = new CompositeAccountAccessor(testAccount);
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
    }
}
