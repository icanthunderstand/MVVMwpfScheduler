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
    public interface IInputFormViewModel
    {
        int Id { get; set; }

        RelayCommand CancelCommand { get; set; }
        RelayCommand DeleteCommand { get; set; }
        RelayCommand SaveCommand { get; set; }
        Event GetEvent();
        void Initialize();
    }
}
