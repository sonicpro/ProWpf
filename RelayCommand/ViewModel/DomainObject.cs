using System.Windows.Input;
using Model;

namespace ViewModel
{
    public class DomainObject
    {
        private RelayCommand _logInCommand;
        private LogInModel _loginModel;

        public DomainObject()
        {
            _logInCommand = new RelayCommand(_ => AttemptLogin(), _ => CanAttemptLogin());
            _loginModel = new LogInModel();
        }

        public string UserName { get; set; }

        public string Password { get; set; }

        /// <summary>
        /// Expose ICommand implementation.
        /// </summary>
        public ICommand LogInCommand => _logInCommand;

        private void AttemptLogin()
        {
            _loginModel.ValidateLogin(UserName, Password);
        }

        private bool CanAttemptLogin()
        {
            return !string.IsNullOrWhiteSpace(UserName) && !string.IsNullOrWhiteSpace(Password);
        }
    }
}
