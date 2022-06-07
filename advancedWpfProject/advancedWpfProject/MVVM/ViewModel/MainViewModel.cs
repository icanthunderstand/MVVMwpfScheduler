using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using advancedWpfProject.Core;
using advancedWpfProject.MVVM.Model;
using System.Collections.ObjectModel;
using advancedWpfProject.Services;


namespace advancedWpfProject.MVVM.ViewModel
{
    public class MainViewModel : ObserverableObject
    {
        private IEventRepository _eventRepository;
        private ILoginRepository _loginRepository;
        
        private ObservableCollection<Event> Events;

        private string _loginId;
        public string LoginId
        {
            get
            {
                return _loginId;
            }
            set
            {
                _loginId = value;
                LoginIdChanged();
                OnPropertyChanged();
            }
        }

        private bool _isLogin;
        public bool IsLogin
        {
            get { return _isLogin; }
            set
            {
                _isLogin = value;
                if (value == true)
                    IsLogout = false;
                else
                    IsLogout = true;
                OnPropertyChanged();
            }
        }
        private bool _isLogout;
        public bool IsLogout
        {
            get { return _isLogout; }
            set
            {
                _isLogout = value;
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

        private DateTime _currentTime;
        public DateTime CurrentTime
        {
            get { return _currentTime; }
            set
            {
                _currentTime = value.Date;                
                OnPropertyChanged();
            }
        }

        private IDaySchedulerViewModel _dayVM;
        private IWeekSchedulerViewModel _weekVM;
        private IMonthSchedulerViewModel _monthVM;
        private IInputFormViewModel _inputFormVM;
        private ILoginFormViewModel _loginFormVM;
        private IRegisterFormViewModel _registerFormVM;
        private IModifyEventFormViewModel _modifyEventFormVM;


        private object _currentView;
        public object CurrentView
        {
            get { return _currentView; }
            set
            {
                _currentView = value;
                UpdateDate(0);
                OnPropertyChanged();
            }
        }

        public RelayCommand DayViewCommand { get; set; }
        public RelayCommand WeekViewCommand { get; set; }
        public RelayCommand MonthViewCommand { get; set; }
        public RelayCommand RightButtonCommand { get; set; }
        public RelayCommand LeftButtonCommand { get; set; }
        public RelayCommand EditEventCommand { get; set; }
        public RelayCommand LoginCommand { get; set; }
        public RelayCommand LogoutCommand { get; set; }

                


        public MainViewModel(IEventRepository eventRepository,ILoginRepository loginRepository,
            IDaySchedulerViewModel dayVM,
            IWeekSchedulerViewModel weekVM,
            IMonthSchedulerViewModel monthVM,
            IInputFormViewModel inputFormVM,
            ILoginFormViewModel loginFormVM,
            IRegisterFormViewModel registerFormVM,
            IModifyEventFormViewModel modifyEventFormVM)
        {
            _eventRepository = eventRepository;
            _loginRepository = loginRepository;
            _dayVM = dayVM;
            _weekVM = weekVM;
            _monthVM = monthVM;
            _inputFormVM = inputFormVM;
            _loginFormVM = loginFormVM;
            _registerFormVM = registerFormVM;
            _modifyEventFormVM = modifyEventFormVM;

            Events = new ObservableCollection<Event>();
            //List<Event> Getlist = _eventRepository.GetEvents();
            //Getlist.ForEach(ev => Events.Add(ev));            

            CurrentTime = DateTime.Now.Date;
            UpdateDate(0);
            UpdateEvents();
            Logout();

            CurrentView = _monthVM;

            
            ////////////////////////////////////////////
            ////각 뷰모델 초기화
            ////////////////////////////////////////////

            _inputFormVM.CancelCommand.AddCommand(o =>
            {
                ChangeView(_monthVM);
            });

            _inputFormVM.DeleteCommand.AddCommand(o =>
            {
                ChangeView(_monthVM);
            });

            _inputFormVM.SaveCommand.AddCommand(o =>
            {
                Event nEvent = _inputFormVM.GetEvent();
                nEvent.LoginId = LoginId;
                if (nEvent != null)
                {
                    Events.Add(nEvent);
                    if (IsLogin)
                    {
                        _eventRepository.AddEvent(nEvent);
                    }
                }
                ChangeView(_monthVM);
            });

            _loginFormVM.LoginCommand.AddCommand(o =>
            {
                if (IsLogout)
                {
                    if (_loginRepository.Login(_loginFormVM.LoginId, _loginFormVM.LoginPassword))
                    {
                        LoginId = _loginFormVM.LoginId;                        
                    }
                    ChangeView(_monthVM);
                }
                else
                {
                    _loginFormVM.FailLogin = true;
                }
            });

            _loginFormVM.RegisterCommand.AddCommand(o =>
            {
                ChangeView(_registerFormVM);
            });





            _monthVM.DayDoubleClickCommand = new RelayCommand(o =>
            {
                if (o == null) return;
                int index = (int)o;
                if (index > -1 & index < 43)
                {
                    _monthVM.SelectedDate = _monthVM.FirstDay.AddDays(index);
                    CurrentTime = _monthVM.SelectedDate;
                }

                ChangeView(_dayVM);
            });

            _monthVM.DayClickCommand = new RelayCommand(o =>
             {
                 if (o == null) return;
                 int index = (int)o;
                 if (index > -1 & index < 43)
                 {
                     _monthVM.SelectedDate = _monthVM.FirstDay.AddDays(index);
                     CurrentTime = _monthVM.SelectedDate;
                 }
                 _monthVM.IsDetail = true;
             });

            _weekVM.DayCommand = new RelayCommand(o =>
             {
                 if (o == null) return;
                 int index = (int)o;
                 if (index > -1 & index < 8)
                     CurrentTime = _weekVM.FirstDay.AddDays(index);
                 CurrentView = _dayVM;
             });


            _registerFormVM.ToLoginCommand.AddCommand(o =>
            {
                ChangeView(_loginFormVM);
            });

            _registerFormVM.RegisterCommand.AddCommand(o =>
            {
                if (_registerFormVM.ValidateData())
                {
                    if(_loginRepository.RegisterAccount(
                        _registerFormVM.LoginId,
                        _registerFormVM.LoginPassword,
                        _registerFormVM.NickName))
                    {
                        ChangeView(_loginFormVM);
                    }
                    else
                    {
                        _registerFormVM.FailRegister = true;
                    }

                }
                else
                {
                    _registerFormVM.FailRegister = true;
                }
            });


            _weekVM.DeleteCommand.AddCommand(o =>
            {
                
                Event parameter = (Event)o;
                if(Events.Any(ev => ev.Id == parameter.Id))
                {
                    for (int i = 0; i < Events.Count(); i++)
                    {
                        if (Events[i].Id == parameter.Id)
                        {
                            Events.RemoveAt(i);
                            break;
                        }                            
                    }
                    _eventRepository.DeleteEvent(parameter.Id);
                }
            });

            _dayVM.DeleteCommand.AddCommand(o =>
            {

                Event parameter = (Event)o;
                if (Events.Any(ev => ev.Id == parameter.Id))
                {
                    for (int i = 0; i < Events.Count(); i++)
                    {
                        if (Events[i].Id == parameter.Id)
                        {
                            Events.RemoveAt(i);
                            break;
                        }
                    }
                    _eventRepository.DeleteEvent(parameter.Id);
                }
            });

            _weekVM.ModifyCommand.AddCommand(o =>
            {
                Event parameter = (Event)o;

                _modifyEventFormVM.InitializeEvent(parameter);
                ChangeView(_modifyEventFormVM);
            });

            _dayVM.ModifyCommand.AddCommand(o =>
            {
                Event parameter = (Event)o;

                _modifyEventFormVM.InitializeEvent(parameter);
                ChangeView(_modifyEventFormVM);
            });

            _modifyEventFormVM.SaveCommand.AddCommand(o =>
            {
                Event mEvent = _modifyEventFormVM.GetEvent();
                if(mEvent != null)
                {
                    if (_eventRepository.UpdateEvent(mEvent))
                    {
                        for (int i = 0; i < Events.Count(); i++)
                        {
                            if (Events[i].Id == mEvent.Id)
                            {
                                Events.RemoveAt(i);
                                Events.Add(mEvent);
                                break;
                            }
                        }
                    }
                }
                ChangeView(_dayVM);
            });

            _modifyEventFormVM.CancelCommand.AddCommand(o =>
            {
                ChangeView(_dayVM);
            });





            /////////////////////////////////////////////////////////////////////////////////////////////////
            ///// 메인뷰모델 커맨드 초기화
            /////////////////////////////
            DayViewCommand = new RelayCommand(o =>
            {
                ChangeView(_dayVM);
            });

            WeekViewCommand = new RelayCommand(o =>
            {
                ChangeView(_weekVM);
            });

            MonthViewCommand = new RelayCommand(o =>
            {
                ChangeView(_monthVM);
            });

            RightButtonCommand = new RelayCommand(o =>
            {
                UpdateDate(1);
            });

            LeftButtonCommand = new RelayCommand(o =>
            {
                UpdateDate(-1); 
            });

            EditEventCommand = new RelayCommand(o =>
            {
                ChangeView(_inputFormVM);
            });

            LoginCommand = new RelayCommand(o =>
            {
                ChangeView(_loginFormVM);
            });

            LogoutCommand = new RelayCommand(o =>
            {
                Logout();
                ChangeView(_monthVM);
            });

        }

        private void UpdateDate(int d)
        {
            if (CurrentView == _monthVM) CurrentTime = CurrentTime.AddMonths(d);
            else if (CurrentView == _weekVM) CurrentTime = CurrentTime.AddDays(7 * d);
            else if (CurrentView == _dayVM) CurrentTime = CurrentTime.AddDays(d);
            

            _dayVM.ShowDay = CurrentTime;
            _weekVM.FirstDay = CurrentTime;
            _monthVM.CurrentMonth = CurrentTime;
        }
        private void Logout()
        {
            LoginId = "Guest";
            NickName = "Guest";

            IsLogin = false;
        }
        private void LoginIdChanged()
        {
            Events.Clear();
            Events = new ObservableCollection<Event>();
            NickName = _loginRepository.GetNickName(LoginId);

            var temp = _eventRepository.GetEvents(LoginId);
            foreach(var element in temp)
            {
                Events.Add(element);
            }

            UpdateEvents();
            IsLogin = true;
        }

        private void UpdateEvents()
        {
            _dayVM.Events = Events;
            _weekVM.Events = Events;
            _monthVM.Events = Events;
            _inputFormVM.Id = CreateId();
        }

        private int CreateId()
        {
            return _eventRepository.GetNextEventId();
        }

        private void ChangeView(object vm)
        {
            _monthVM.IsDetail = false;
            _loginFormVM.FailLogin = false;
            _inputFormVM.Initialize();
            _registerFormVM.Initialize();
            _loginFormVM.Initialize();
            
            CurrentView = vm;
        }

    }
}
