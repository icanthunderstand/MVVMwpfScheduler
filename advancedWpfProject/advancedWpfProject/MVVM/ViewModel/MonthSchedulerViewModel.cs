using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using advancedWpfProject.MVVM.Model;
using System.Collections.ObjectModel;
using advancedWpfProject.Core;
using advancedWpfProject.Services;
using System.Collections.Specialized;



namespace advancedWpfProject.MVVM.ViewModel
{
    public class MonthSchedulerViewModel : ObserverableObject ,IMonthSchedulerViewModel
    {
        private ObservableCollection<Event> _events;
        public ObservableCollection<Event> Events
        {
            get { return _events; }
            set
            {
                try
                {
                    _events = value;
                    _events.CollectionChanged += EventsCollectionChanged;
                    OnPropertyChanged();
                    InitializeEvents();
                }
                catch
                {
                    return;
                }
            }
        }

        private DateTime _firstDay;
        public DateTime FirstDay
        {
            get { return _firstDay; }
            set
            {
                _firstDay = value.Date;
                OnPropertyChanged();
            }
        }

        private DateTime _currentMonth;
        public DateTime CurrentMonth
        {
            get { return _currentMonth; }
            set
            {                
                try
                {
                    _currentMonth = new DateTime(value.Year, value.Month, 1);
                    AdjustFirstDay(); 
                    InitializeEvents();
                    OnPropertyChanged();
                }
                catch
                {
                    return;
                }
            }
        }


        private ObservableCollection<MonthList> _monthLists;
        public ObservableCollection<MonthList> MonthLists
        {
            get { return _monthLists; }
            set
            {
                _monthLists = value;
                OnPropertyChanged();
            }
        }

        private List<Event> _detailList;
        public List<Event> DetailList
        {
            get { return _detailList; }
            set
            {
                _detailList = value;
                OnPropertyChanged();
            }
        }


        private DateTime _selectedDate { get; set; }
        public DateTime SelectedDate
        {
            get { return _selectedDate; }
            set
            {
                _selectedDate = value;
                SelectedDateChanged();
                OnPropertyChanged();
            }
        }
        private bool _isDetail;
        public bool IsDetail
        {
            get { return _isDetail; }
            set
            {
                _isDetail = value;
                OnPropertyChanged();
            }
        }



        public RelayCommand DayClickCommand { get; set; }
        public RelayCommand DayDoubleClickCommand { get; set; }

        


        public MonthSchedulerViewModel()
        {
            MonthLists = new ObservableCollection<MonthList>();
            IsDetail = false;
        }

        private void EventsCollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            InitializeEvents();
        }
        private void AdjustFirstDay()
        {
            DateTime tempDay = CurrentMonth;

            while (tempDay.DayOfWeek != DayOfWeek.Sunday)
                tempDay = tempDay.AddDays(-1);

            FirstDay = tempDay;
        }

        private void InitializeEvents()
        {
            MonthLists.Clear();
            int maxEvent = 5;

            DateTime currentDay = FirstDay;
            for (int i = 0; i < (6 * 7); i++)
            {
                //해당날짜에서 보일 리스트
                var DayEvents = Events.Where(e => e.StartTime < currentDay.AddDays(1) & e.EndTime > currentDay).OrderBy(e => e.StartTime);

                MonthList DayList = new MonthList();
                DayList.Index = i;

                int count = 0;
                foreach(var dayEvent in DayEvents)
                {
                    DayList.List.Add(dayEvent);
                    if (++count > maxEvent) break;
                }

                MonthLists.Add(DayList);
                currentDay = currentDay.AddDays(1);
            }
        }

        private void SelectedDateChanged()
        {
            DateTime showDay = SelectedDate;

            //해당날짜에서 보일 리스트
            var DayEvents = Events.Where(e => e.StartTime < showDay.AddDays(1) & e.EndTime > showDay).OrderBy(e => e.StartTime);

            List<Event> tempList = new List<Event>();

            foreach (var ele in DayEvents)
            {
                tempList.Add(ele);
            }

            DetailList = tempList;
        }


    }
}
