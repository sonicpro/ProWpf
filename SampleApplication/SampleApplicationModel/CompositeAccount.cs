using System;
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

        public CompositeAccount(string name) : this()
        {
            Name = name;
        }

        public string Name { get; }

        public Money Balance { get; }

        public List<IAccount> ChildAccounts { get; }

        public IAccount Parent { get; private set; }

        public void AddAccount(IAccount account)
        {
            if (account.Parent != null)
            {
                throw new InvalidOperationException("Cannot change the account belonging. It must be removed from the hierarchy first.");
            }

            if (ChildAccounts.Any(ca => ca.Name?.Equals(account.Name, StringComparison.CurrentCulture) ?? false))
            {
                throw new InvalidOperationException($"There is already a \"{account.Name}\" account here. You cannot add the other one.");
            }

            // This check is needed because other IAccount implementers might have readonly Parent property.
            if (!(account is CompositeAccount compositeAccount))
            {
                throw new ArgumentException("The parameter is of unsupported type.", nameof(account));
            }

            if (compositeAccount.IsAncestor(this))
            {
                throw new InvalidOperationException("Account hierarchy cannot be cyclic.");
            }

            ChildAccounts.Add(account);
            compositeAccount.Parent = this;
        }

        public void RemoveAccount(IAccount account)
        {
            ChildAccounts.Remove(account);
        }

        /// <summary>
        /// Verifies if this account is the ancestor of an account.
        /// </summary>
        /// <param name="account">An account to test.</param>
        /// <returns>True if the this is the ancestor of an account, false otherwise.</returns>
        private bool IsAncestor(IAccount account)
        {
            bool returnValue = false;
            var ancestor = account.Parent;
            while (ancestor != null)
            {
                if (ancestor == this)
                {
                    returnValue = true;
                    break;
                }

                ancestor = ancestor.Parent;
            }

            return returnValue;
        }
    }
}
