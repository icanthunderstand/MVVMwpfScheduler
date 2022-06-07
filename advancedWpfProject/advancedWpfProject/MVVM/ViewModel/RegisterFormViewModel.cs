using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using advancedWpfProject.Core;
using advancedWpfProject.Services;

namespace advancedWpfProject.MVVM.ViewModel
{
    public class RegisterFormViewModel : ObserverableObject, IRegisterFormViewModel
    {
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

        private string _verifyPassword;
        public string VerifyPassword
        {
            get { return _verifyPassword; }
            set
            {
                _verifyPassword = value;
                OnPropertyChanged();
            }
        }

        private bool _failRegister;
        public bool FailRegister
        {
            get { return _failRegister; }
            set
            {
                _failRegister = value;
                OnPropertyChanged();
            }
        }

        private string _nickName;
        public string NickName
        {
            get { return _nickName; }
            set
            {
                _nickName = value;
                OnPropertyChanged();
            }
        }


        public RelayCommand ToLoginCommand { get; set; }

        public RelayCommand RegisterCommand { get; set; }

        public RegisterFormViewModel()
        {
            ToLoginCommand = new RelayCommand(o =>
            {

            });

            RegisterCommand = new RelayCommand(o =>
            {

            });
            Initialize();
        }



        public bool ValidateData()
        {
            if(!LoginId.Equals(string.Empty) &
                !LoginPassword.Equals(string.Empty) &
                !VerifyPassword.Equals(string.Empty) &
                !NickName.Equals(string.Empty) &
                !NickName.Equals("Guest") &
                LoginPassword.Equals(VerifyPassword))
            {
                return true;
            }

            return false;
        }

        public void Initialize()
        {
            FailRegister = false;
            LoginId = string.Empty;
            LoginPassword = string.Empty;
            VerifyPassword = string.Empty;
            NickName = string.Empty;
        }
        
    }
}
