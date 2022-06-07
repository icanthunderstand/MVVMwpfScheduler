﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using advancedWpfProject.Core;
using System.Windows.Media;
using advancedWpfProject.MVVM.Model;
using System.Reflection;
using System.Collections.ObjectModel;
using advancedWpfProject.Services;


namespace advancedWpfProject.MVVM.ViewModel
{
    public class InputFormViewModel : ObserverableObject , IInputFormViewModel
    {
        public RelayCommand DeleteCommand { get; set; }
        public RelayCommand SaveCommand { get; set; }
        public RelayCommand CancelCommand { get; set; }

        public string LoginId { get; set; }

        #region properties
        private string _title;
        public string Title
        {
            get { return _title; }
            set
            {
                _title = value;
                OnPropertyChanged();
            }
        }

        private string _location;
        public string Location
        {
            get { return _location; }
            set
            {
                _location = value;
                OnPropertyChanged();
            }
        }
        private string _description;
        public string Description
        {
            get { return _description; }
            set
            {
                _description = value;
                OnPropertyChanged();
            }
        }
        private string _foreground;
        public string Foreground
        {
            get { return _foreground; }
            set
            {
                _foreground = value;
                OnPropertyChanged();
            }
        }

        private string _background;
        public string Background
        {
            get { return _background; }
            set
            {
                _background = value;
                OnPropertyChanged();
            }
        }


        private DateTime _selectedStartDate;
        public DateTime SelectedStartDate
        {
            get { return _selectedStartDate; }
            set
            {
                _selectedStartDate = value.Date;
                OnPropertyChanged();
            }
        }

        private int _selectedStartTime;
        public int SelectedStartTime 
        {
            get { return _selectedStartTime; }
            set
            {
                _selectedStartTime = value;
                OnPropertyChanged();
            }
        }


        private DateTime _selectedEndDate;
        public DateTime SelectedEndDate
        {
            get { return _selectedEndDate; }
            set
            {
                _selectedEndDate = value.Date;
                OnPropertyChanged();
            }
        }


        private int _selectedEndTime;
        public int SelectedEndTime
        {
            get { return _selectedEndTime; }
            set
            {
                _selectedEndTime = value;
                OnPropertyChanged();
            }
        }
        #endregion

        public int Id { get; set; }   


        public InputFormViewModel()
        {
            LoginId = string.Empty;
            //Event = new Event();
            SelectedStartDate = DateTime.Now;
            SelectedEndDate = DateTime.Now;
            SelectedStartTime = -1;
            SelectedEndTime = -1;            

            DeleteCommand = new RelayCommand(o =>
            { 
                Initialize();
            });

            SaveCommand = new RelayCommand(o =>
            {
            });

            CancelCommand = new RelayCommand(o =>
            {

            });
        }

        public Event GetEvent()
        {
            if (ValidateData())
            {
                return new Event()
                {
                    Id = Id++,
                    Title = Title,
                    Location = Location,
                    Description = Description,
                    StartTime = GetStartTime(),
                    EndTime = GetEndTime(),
                    Foreground = Foreground,
                    Background = Background
                };
            }

            return null;            
        }

        private bool ValidateData()
        {
            if (GetStartTime() >= GetEndTime() &
                Title.Equals(string.Empty))
            {
                return false;
            }

            return true;
        }

        private DateTime GetStartTime()
        {
            DateTime temp = SelectedStartDate;
            for(int i=0;i<SelectedStartTime;i++)
            {
                temp=temp.AddMinutes(30);
            }
            return temp;              
        }

        private DateTime GetEndTime()
        {
            DateTime temp = SelectedEndDate;
            for (int i = 0; i < SelectedEndTime; i++)
            {
                temp = temp.AddMinutes(30);
            }
            return temp;
        }

        public void Initialize()
        {
            //Id
            Title = String.Empty;
            Location = String.Empty;
            Description = string.Empty;
            Foreground = Colors.Black.ToString();
            Background = Colors.White.ToString();
            SelectedStartDate = DateTime.Now;
            SelectedEndDate = DateTime.Now;
            SelectedStartTime = -1;
            SelectedEndTime = -1;
        }

        
        

    }
}
