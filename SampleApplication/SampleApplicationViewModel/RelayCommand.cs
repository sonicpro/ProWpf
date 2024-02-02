using System;
using System.Windows.Input;

namespace SampleApplicationViewModel
{
    public class RelayCommand : ICommand
    {
        #region Constructors

        public RelayCommand(Action<object> execute, Predicate<object> canExecute)
        {
            executeHandler = execute;
            canExecuteHandler = canExecute;
        }

        public RelayCommand(Action<object> execute)
            : this(execute, null)
        {

        }

        #endregion

        #region ICommand Implementation

        public bool CanExecute(object parameter)
        {
            bool canExecuteValue = true;
            if (this.canExecuteHandler != null)
            {
                canExecuteValue = this.canExecuteHandler(parameter);
            }
            return canExecuteValue;
        }

        public event EventHandler CanExecuteChanged
        {
            // System.Windows.Input.CommandManager detects UI changes, like the focus moves out.
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }

        public void Execute(object parameter)
        {
            executeHandler(parameter);
        }

        #endregion

        #region Fields

        private readonly Action<object> executeHandler;
        private readonly Predicate<object> canExecuteHandler;

        #endregion
    }
}