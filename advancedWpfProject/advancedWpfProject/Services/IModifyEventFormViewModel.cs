using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using advancedWpfProject.Core;
using advancedWpfProject.MVVM.Model;


namespace advancedWpfProject.Services
{
    public interface IModifyEventFormViewModel
    {
        RelayCommand SaveCommand { get; set; }
        RelayCommand CancelCommand { get; set; }
        void InitializeEvent(Event ev);
        Event GetEvent();

    }
}
