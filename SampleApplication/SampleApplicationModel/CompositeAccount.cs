using System.Collections.Generic;
using System.Linq;

namespace SampleApplicationModel
{
    public class CompositeAccount : IAccount
    {
        public CompositeAccount()
        {
            ChildAccounts = new List<IAccount>();
        }

        public string Name { get; }

        public Money Balance { get; }

        public List<IAccount> ChildAccounts { get; }

        public void AddAccount(IAccount account)
        {
            ChildAccounts.Add(account);
        }

        public void RemoveAccount(IAccount account)
        {
            ChildAccounts.Remove(account);
        }
    }
}
