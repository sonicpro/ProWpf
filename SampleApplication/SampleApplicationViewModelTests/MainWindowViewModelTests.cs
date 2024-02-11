using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using SampleApplicationModel;
using SampleApplicationViewModel;

namespace SampleApplicationViewModelTests
{
    [TestClass]
    public class MainWindowViewModelTests
    {
        [TestMethod]
        public void TestMainWindowViewModelDelegatesToIPerson()
        {
            var mockPerson = new Mock<IPerson>();
            mockPerson.Setup(p => p.NetWorth).Returns(Money.Zero);
            var viewModel = new MainWindowViewModel(mockPerson.Object);

            var netWorth = viewModel.NetWorth;
            mockPerson.Verify(p => p.NetWorth, Times.Once);
        }
    }
}
