using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using advancedWpfProject.Core;


namespace advancedWpfProject.Services
{
    public interface IRegisterFormViewModel 
    {
        string LoginId { get; set; }
        string LoginPassword { get; set; }
        string VerifyPassword { get; set; }
        string NickName { get; set; }
        RelayCommand ToLoginCommand { get; set; }

        RelayCommand RegisterCommand { get; set; }
        bool FailRegister { get; set; }
        void Initialize();
        bool ValidateData();

    }
}
