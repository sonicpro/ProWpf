using System;
using System.ComponentModel;
using System.Windows.Input;
using MvvmWpfAppApp.Model;

namespace MvvmWpfApp.ViewModel
{
    public class SampleViewModel : INotifyPropertyChanged
    {
        private readonly SampleModel model;
        private RelayCommand calculateSquareRootCommand;
        private double result;

        public event PropertyChangedEventHandler PropertyChanged;

        public SampleViewModel()
        {
            model = new SampleModel();
        }

        public double Result
        {
            // ReSharper disable once ArrangeAccessorOwnerBody
            get { return result; }
            private set
            {
                if (Math.Abs(result - value) < 0.0001)
                {
                    return;
                }

                result = value;
                if (PropertyChanged != null)
                {
                    PropertyChanged(this, new PropertyChangedEventArgs(nameof(Result)));
                }
            }
        }

        public double Number { get; set; }

        public ICommand CalculateSquareRootCommand
        {
            get
            {
                if (calculateSquareRootCommand == null)
                {
                    calculateSquareRootCommand = new RelayCommand(parameter =>
                        CalculateSquareRoot());
                }

                return calculateSquareRootCommand;
            }
        }

        #region Methods

        private void CalculateSquareRoot()
        {
            Result = model.CalculateSquareRoot(Number);
        }

        #endregion
    }
}
