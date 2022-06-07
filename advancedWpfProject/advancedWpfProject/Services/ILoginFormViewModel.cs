using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;
using advancedWpfProject.MVVM.Model;
using advancedWpfProject.Core;

namespace advancedWpfProject.Services
{
    public interface ILoginFormViewModel
    {
        RelayCommand LoginCommand { get; set; }
        RelayCommand RegisterCommand { get; set; }
        string LoginId { get; set; }
        string LoginPassword { get; set; }
        bool FailLogin { get; set; }

    }
}
