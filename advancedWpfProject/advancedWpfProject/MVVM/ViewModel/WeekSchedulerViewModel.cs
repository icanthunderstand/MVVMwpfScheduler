using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;
using advancedWpfProject.MVVM.Model;
using advancedWpfProject.Core;
using System.Windows.Media;
using advancedWpfProject.Services;
using System.Collections.Specialized;

namespace advancedWpfProject.MVVM.ViewModel
{
    public class WeekSchedulerViewModel : ObserverableObject , IWeekSchedulerViewModel
    {
        public ObservableCollection<Event> _events;
        public ObservableCollection<Event> Events
        {
            get { return _events; }
            set
            {
                try
                {
                    _events = value;
                    _events.CollectionChanged += EventsCollectionChanged;
                    InitializeEvents();
                    OnPropertyChanged();
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
                try
                {
                    while (value.DayOfWeek != DayOfWeek.Sunday)
                        value = value.AddDays(-1);
                    _firstDay = value.Date;

                    InitializeEvents();
                    OnPropertyChanged();
                }
                catch
                {
                    return;
                }
            }
        }

        //view에서 보여줄 리스트
        public ObservableCollection<ObservableCollection<MergeList>> WeekEvents { get; set; } 

        public RelayCommand DayCommand { get; set; }
        public RelayCommand ModifyCommand { get; set; }
        public RelayCommand DeleteCommand { get; set; }


        public WeekSchedulerViewModel()
        {
            WeekEvents = new ObservableCollection<ObservableCollection<MergeList>>();

            ModifyCommand = new RelayCommand(o =>
            {

            });
            DeleteCommand = new RelayCommand(o =>
            {

            });
            
        }

        private void EventsCollectionChanged(object sender,NotifyCollectionChangedEventArgs e)
        {
            InitializeEvents();
        }

        public void InitializeEvents()
        {
            WeekEvents.Clear();
            DateTime firstDay = this.FirstDay;

            for (int i = 0; i < 7; i++)
            {
                //해당날짜에서 보일 리스트
                var DayEvents = Events.Where(e => e.StartTime < firstDay.AddDays(1) & e.EndTime > firstDay).OrderBy(e => e.StartTime);
                

                ObservableCollection<MergeList> MergeLists = new ObservableCollection<MergeList>();                

                foreach(Event ev in DayEvents)
                {
                    DateTime StartTime = ev.StartTime.Day < firstDay.Day ? firstDay : ev.StartTime;
                    DateTime EndTime = ev.EndTime.Day > firstDay.Day ? firstDay.AddDays(1) : ev.EndTime;

                    Event ShowEvent = new Event()
                    {
                        Title = ev.Title,
                        Location = ev.Location,
                        StartTime = StartTime,
                        EndTime = EndTime,
                        Description = ev.Description,
                        LoginId = ev.LoginId,
                        Background = ev.Background,
                        Foreground = ev.Foreground,
                        Id = ev.Id
                    };

                    MergeLists.Add(new MergeList(ShowEvent, 0, 0, 0));
                }

                //순서대로 n번째 left를 깔아줌
                //정렬기준은 startime 오름차순 정렬 후 endtime 내림차순 정렬
                //모든 이벤트의 left가 확정될까지 반복함
                //모든 이벤트의 width(실제control의)는 최대 mergelist.width * Actualwidth /overlapcount ////  mergelist의 모든 수치는 상대적, 실제 control의 width는 view에서 결정함
                //mergelist.width는 자신과 겹치는 overlaplist에서 자신의 left보다 큰 left까지
                //조건에 맞는얘가 없으면 오른쪽 끝까지

                int iter = 0;                
                while(MergeLists.Where(e => e.OverlapCount == 0).Count() > 0)
                {
                    var temp = MergeLists.Where(e => e.OverlapCount == 0).OrderBy(e => e.Event.StartTime).ThenByDescending(e => e.Event.EndTime);

                    while (temp.Count() > 0)
                    {
                        var first = temp.First();
                        first.Left = iter;
                        first.OverlapCount = 1;

                        temp = temp.Where(e => first.Event.EndTime <= e.Event.StartTime).OrderBy(e => e.Event.StartTime).ThenByDescending(e => e.Event.EndTime);
                    }
                    iter++;
                }

                foreach(var merge in MergeLists)
                {
                    merge.OverlapCount = iter;
                }

                foreach (MergeList merge in MergeLists)
                {
                    var overlapList = MergeLists.Where(e =>
                    {
                        return e.Event.EndTime > merge.Event.StartTime & e.Event.StartTime < merge.Event.EndTime;
                    });

                    var widthList = overlapList.Where(e => merge.Left < e.Left).OrderBy(e => e.Left);

                    if (widthList.Count() > 0)
                    {
                        merge.Width = widthList.First().Left - merge.Left;
                    }
                    else
                    {
                        merge.Width = merge.OverlapCount - merge.Left;
                    }

                }

                WeekEvents.Add(MergeLists);
                firstDay = firstDay.AddDays(1);
            }

        }
        


    }
}
