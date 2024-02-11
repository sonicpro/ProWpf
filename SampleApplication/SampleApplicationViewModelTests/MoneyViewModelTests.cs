using System.Globalization;
using System.Threading;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SampleApplicationModel;
using SampleApplicationViewModel;

namespace SampleApplicationViewModelTests
{
    [TestClass]
    public class MoneyViewModelTests
    {
        public TestContext TestContext { get; set; }
        private Money money;

        [TestInitialize]
        public void OnInitialize()
        {
            if (TestContext.TestName.ToUpperInvariant().Contains("US"))
            {
                Thread.CurrentThread.CurrentCulture = new CultureInfo("en-US");
            }
            else
            {
                Thread.CurrentThread.CurrentCulture = new CultureInfo("en-GB");
            }

            if (TestContext.TestName.Contains("Negative"))
            {
                money = new Money(-123.45M);
            }
            else
            {
                money = new Money(123.45M);
            }
        }

        [TestMethod]
        public void TestGbValueFormatIsCorrect()
        {
            var moneyViewModel = new MoneyViewModel(money);
            Assert.AreEqual("£123.45", moneyViewModel.DisplayValue);
        }

        [TestMethod]
        public void TestNegativeGbValueFormatIsCorrect()
        {
            var moneyViewModel = new MoneyViewModel(money);
            Assert.AreEqual("-£123.45", moneyViewModel.DisplayValue);
        }

        [TestMethod]
        public void TestUsValueFormatIsCorrect()
        {
            var moneyViewModel = new MoneyViewModel(money);
            Assert.AreEqual("$123.45", moneyViewModel.DisplayValue);
        }

        [TestMethod]
        public void TestNegativeUsValueFormatIsCorrect()
        {
            var moneyViewModel = new MoneyViewModel(money);
            Assert.AreEqual("($123.45)", moneyViewModel.DisplayValue);
        }
    }
}
