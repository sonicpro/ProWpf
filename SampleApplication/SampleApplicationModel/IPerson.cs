using System.Collections.Generic;

namespace SampleApplicationModel
{
    public interface IPerson
    {
        void AddAccount(IAccount account);

        Money NetWorth { get; }

        IEnumerable<IAccount> Accounts { get; }
    }
}
