using System;
using System.Windows.Input;

namespace ViewModel
{
    public class RelayCommand : ICommand
    {
        private readonly Action<object> _executeHandler;
        private readonly Predicate<object> _canExecuteHandler;

        public RelayCommand(Action<object> executeHandler)
        {
            _executeHandler = executeHandler;
        }

        public RelayCommand(Action<object> executeHandler, Predicate<object> canExecuteHandler) : this(executeHandler)
        {
            _canExecuteHandler = canExecuteHandler;
        }

        public bool CanExecute(object parameter)
        {
            return _canExecuteHandler == null || _canExecuteHandler(parameter);
        }

        public void Execute(object parameter)
        {
            _executeHandler(parameter);
        }

        public event EventHandler CanExecuteChanged
        {
            add
            {
                CommandManager.RequerySuggested += value;
            }

            remove
            {
                CommandManager.RequerySuggested -= value;
            }
        }
    }
}
