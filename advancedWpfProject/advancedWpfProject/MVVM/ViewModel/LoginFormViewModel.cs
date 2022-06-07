using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using advancedWpfProject.Core;
using advancedWpfProject.Services;


namespace advancedWpfProject.MVVM.ViewModel
{
    public class LoginFormViewModel : ObserverableObject, ILoginFormViewModel
    {
        private ILoginRepository _loginRepository;

        private string _loginId;
        public string LoginId
        {
            get { return _loginId; }
            set
            {
                _loginId = value;
                OnPropertyChanged();
            }
        }
        private string _loginPassword;
        public string LoginPassword
        {
            get { return _loginPassword; }
            set
            {
                _loginPassword = value;
                OnPropertyChanged();
            }
        }

        public RelayCommand LoginCommand { get; set; }
        public RelayCommand RegisterCommand { get; set; }

        private bool _failLogin;
        public bool FailLogin
        {
            get { return _failLogin; }
            set
            {
                _failLogin = value;
                OnPropertyChanged();
            }
        }



        public LoginFormViewModel(ILoginRepository loginRepository)
        {
            _loginRepository = loginRepository;

            LoginCommand = new RelayCommand(o =>
            {

            });

            RegisterCommand = new RelayCommand(o =>
            {

            });
        }



    }
}
