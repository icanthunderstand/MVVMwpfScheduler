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
    public interface IMonthSchedulerViewModel
    {
        ObservableCollection<Event> Events { get; set; }
        DateTime CurrentMonth { get; set; }        
        DateTime FirstDay { get; set; }
        DateTime SelectedDate { get; set; }
        bool IsDetail { get; set; }
        RelayCommand DayDoubleClickCommand { get; set; }
        RelayCommand DayClickCommand { get; set; }
    }
}
