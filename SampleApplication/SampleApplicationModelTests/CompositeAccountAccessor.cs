using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SampleApplicationModel;

namespace SampleApplicationModelTests
{
    class CompositeAccountAccessor
    {
        private readonly PrivateType testedType = new PrivateType(typeof(CompositeAccount));
        private readonly PrivateObject testedObject;

        internal CompositeAccountAccessor(CompositeAccount target)
        {
            testedObject = new PrivateObject(target, testedType);
        }

        internal List<IAccount> ChildAccounts
        {
            get => (List<IAccount>) testedObject.GetProperty("ChildAccounts");
            set => testedObject.SetField("<ChildAccounts>k__BackingField", value);
        }
    }
}
