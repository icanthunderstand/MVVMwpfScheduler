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
    public interface IDaySchedulerViewModel
    {
        ObservableCollection<Event> Events { get; set; }
        DateTime ShowDay { get; set; }
        RelayCommand ModifyCommand { get; set; }
        RelayCommand DeleteCommand { get; set; }
    }
}
