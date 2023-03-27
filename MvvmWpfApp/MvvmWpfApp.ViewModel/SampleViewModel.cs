using MvvmWpfAppApp.Model;

namespace MvvmWpfApp.ViewModel
{
    public class SampleViewModel
    {
        public double Result { get; private set; }

        public double Number { get; set; }

        public void CalculateSquareRootCommand()
        {
            var model = new SampleModel();
            Result = model.CalculateSquareRoot(Number);
        }
    }
}
