using System.CodeDom;
using System.Collections;
using System.Collections.Generic;

namespace SampleApplicationModel
{
    class Person : IPerson, IEnumerable<IAccount>
    {
        private readonly ICollection<IAccount> accounts = new List<IAccount>();

        public void AddAccount(IAccount account)
        {
            accounts.Add(account);
        }

        public Money NetWorth
        {
            get { return new Money(); }
        }

        public IEnumerable<IAccount> Accounts => accounts;

        public IEnumerator<IAccount> GetEnumerator()
        {
            return accounts.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}
