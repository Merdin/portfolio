using PitchPerfectHearingTest.Commands;
using PitchPerfectHearingTest.DataAccess;
using PitchPerfectHearingTest.DataAccess.Repositories;
using PitchPerfectHearingTest.Models;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace PitchPerfectHearingTest.ViewModels
{
    public class AppointmentSelectTimeDialogViewModel:BaseDialogViewModel
    {
        public event EventHandler<ChangeViewEventArgs> ViewChangeRequest;
        public ICommand OpenAddContactInfoViewCommand { get; set; }

        private string _showLabelSelectTime;
        public string ShowLabelSelectTime { get { return _showLabelSelectTime; } set { _showLabelSelectTime = value; OnPropertyChanged("ShowLabelSelectTime");} }

        private string _selectedDateLabel;
        public string SelectedDateLabel
        {
            get { return _selectedDateLabel;}
            set
            {
                _selectedDateLabel = value;
                base.OnPropertyChanged("SelectedDateLabel");
            }
        }

        private List<TimeSlot> _timeSlots = new List<TimeSlot>();
        public List<TimeSlot> TimeSlots {
            get { return _timeSlots; }
            set{ _timeSlots = value; OnPropertyChanged("TimeSlots"); }
            }

        public AppointmentSelectTimeDialogViewModel()
        {
            OpenAddContactInfoViewCommand = new RelayCommand(selectedTimeSlot => SwitchView((TimeSlot)selectedTimeSlot));
            ShowLabelSelectTime = "Hidden";
        }
        
        private void SwitchView(TimeSlot selectedTimeSlot)
        {
            SessionContext.SelectedTimeSlot = selectedTimeSlot;
            ViewChangeRequest?.Invoke(this, new ChangeViewEventArgs(nameof(AppointmentAddContactInfoViewModel)));
        }
        private void SetSelectedDateLabel(DateTime selectedDate)
        {
            var weekDayConverter = new WeekDayConverter();
            var sb = new StringBuilder();
            //sb.Append("Selecteer een tijd op ");
            sb.Append(weekDayConverter.DayOfWeekDutch(selectedDate.DayOfWeek));
            sb.Append(" ");
            sb.Append(selectedDate.Day);
            sb.Append(" ");
            sb.Append(selectedDate.ToString("MMMM", CultureInfo.InvariantCulture));
            sb.Append("\n");
            SelectedDateLabel = sb.ToString();
        }

       
        //Populate itemscontrol with timeslots from selected data in database
        public void OnSelectedDate(object sender,SelectionChangedEventArgs e)
        {
            ShowLabelSelectTime = "Visible";
            //get date from datepicker
            var DatePicker = sender as DatePicker;
            //set selected date label
            SetSelectedDateLabel(DatePicker.SelectedDate.Value); 
            //Initialize instance of type TImeslot for selected date
            TimeSlot selectedDate = new TimeSlot();
            // Set instance of starttime time with selected date
            selectedDate.StartTime = DatePicker.SelectedDate.Value;

            using (var context = new ApplicationContext())
            {
                var timeSlotRepository = new TimeSlotRepository(context);
                //Grab all available timeslots from database with selected date
                TimeSlots = (List<TimeSlot>)timeSlotRepository.GetSelection(selectedDate);

                foreach (var t in TimeSlots)
                {
                    var sb = new StringBuilder();
                    {
                        sb.Append(t.StartTime.ToLocalTime());
                        sb.Append("-");
                        sb.Append(t.EndTime.ToLocalTime());
                    }
                }
            }
        }
    }
}
