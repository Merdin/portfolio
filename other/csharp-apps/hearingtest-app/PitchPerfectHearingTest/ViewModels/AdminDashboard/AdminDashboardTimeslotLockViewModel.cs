using PitchPerfectHearingTest.Models;
using PitchPerfectHearingTest.Commands;
using PitchPerfectHearingTest.DataAccess;
using PitchPerfectHearingTest.DataAccess.Repositories;
using PitchPerfectHearingTest.Dialogs.DialogService;
using PitchPerfectHearingTest.Dialogs.DialogYesNo;
using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Collections.Generic;

namespace PitchPerfectHearingTest.ViewModels
{
    public class AdminDashboardTimeslotLockViewModel : BaseViewModel
    {
        //private DialogBaseViewModel _vm;

        private ICommand _selectedTimeSlotCommand = null;

        private bool _enableTime1 = false;
        private bool _enableTime2 = false;
        private bool _enableBtn1 = false;
        private bool _enableBtn2 = false;
        public bool EnableBtn1 { get { return _enableBtn1; } set { _enableBtn1 = value; base.OnPropertyChanged("EnableBtn1"); } }
        public bool EnableBtn2 { get { return _enableBtn2; } set { _enableBtn2 = value; base.OnPropertyChanged("EnableBtn2"); } }

        private string _setComboboxIndex;

        private string _selectedStartDateAndTime;
        private string _selectedEndDateAndTime;

        private DateTime _startDateAndTime;
        private DateTime _endDateAndTime;

        public event EventHandler<ChangeViewEventArgs> ChangeView;

        public AdminDashboardTimeslotLockViewModel()
        {
            _selectedTimeSlotCommand = new RelayCommand(OnSelectedTimeSlotCommand);
        }
        public bool EnableTime1
        {
            get { return _enableTime1; }
            set { _enableTime1 = value; base.OnPropertyChanged("EnableTime1"); }
        }

        public bool EnableTime2
        {
            get { return _enableTime2; }
            set { _enableTime2 = value; base.OnPropertyChanged("EnableTime2"); }
        }
        public string SetComboBoxIndex
        {
            get
            {
                if (String.IsNullOrEmpty(_setComboboxIndex))
                {
                    return "0";
                }
                else
                {
                    return _setComboboxIndex;
                }

            }
            set { _setComboboxIndex = value; base.OnPropertyChanged("SetComboBoxIndex"); }
        }

        public string SelectedStartDateAndTime
        {
            get { return _selectedStartDateAndTime; }
            set { _selectedStartDateAndTime = value; base.OnPropertyChanged("SelectedStartDateAndTime"); }
        }
        public string SelectedEndDateAndTime
        {
            get { return _selectedEndDateAndTime; }
            set { _selectedEndDateAndTime = value; base.OnPropertyChanged("SelectedEndDateAndTime"); }
        }

        public ICommand ConfirmSelectedTimeSlotCommand
        {
            get { return _selectedTimeSlotCommand; }
            set { _selectedTimeSlotCommand = value; }
        }

        private void OnSelectedTimeSlotCommand(object parameter)
        {

            TimeSlotPeriod timeSlot = new TimeSlotPeriod(_startDateAndTime, _endDateAndTime);

            DialogBaseViewModel vm = new DialogYesNoViewModel("Tijdslot vastzetten?\n" +
                                                              timeSlot.StartPeriod.ToString() + "\n"+
                                                              timeSlot.EndPeriod.ToString());

            //Result from OpenDialog in Dialogservice
            DialogResult result = Dialogs.DialogService.DialogService.OpenDialog(vm, parameter as Window);
            if (result == DialogResult.Yes)
            {
                PersistLockedTimeSlotAndAdminAppointment(timeSlot);
                EnableBtn2 = true;
            }
            MessageBox.Show("TijdSlot vastgezet!");
            ResetValues();  
        }

        public void OnSelectedStartDate(object sender, SelectionChangedEventArgs e)
        {
            var datePicker = sender as DatePicker;
            DateTime date = datePicker.SelectedDate.Value;
            SelectedStartDateAndTime = date.ToShortDateString();

            EnableTime1 = true;
        }
        public void OnSelectedEndDate(object sender, SelectionChangedEventArgs e)
        {
            var datePicker = sender as DatePicker;
            DateTime date = datePicker.SelectedDate.Value;
            SelectedEndDateAndTime = date.ToShortDateString();
            EnableTime2 = true;

        }

        public void OnSelectedStartTime(object sender, SelectionChangedEventArgs e)
        {
            var comboBoxTime = sender as ComboBox;
            if (!(comboBoxTime.SelectedItem == null))
            {
                ComboBoxItem selectedItem = comboBoxTime.SelectedItem as ComboBoxItem;
                string selectedItemString = selectedItem.Content.ToString();
                if (selectedItemString != "Kies tijd")
                {
                    SelectedStartDateAndTime += " ";
                    SelectedStartDateAndTime += selectedItemString;
                    _startDateAndTime = ParseStringToDate(SelectedStartDateAndTime);
                }
            }
            EnableTime1 = false;

        }
        public void OnSelectedEndTime(object sender, SelectionChangedEventArgs e)
        {
            var comboBoxTime = sender as ComboBox;
            if (!(comboBoxTime.SelectedItem == null))
            {
                ComboBoxItem selectedItem = comboBoxTime.SelectedItem as ComboBoxItem;
                string selectedItemString = selectedItem.Content.ToString();
                if (selectedItemString != "Kies tijd")
                {
                    SelectedEndDateAndTime += " ";
                    SelectedEndDateAndTime += selectedItemString;
                    _endDateAndTime = ParseStringToDate(SelectedEndDateAndTime);
                    EnableBtn1 = true;
                }
            }
        }

        private void PersistLockedTimeSlotAndAdminAppointment(TimeSlotPeriod timeSlotPeriod)
        {
            TimeSlotRepository timeSlotRepository = new TimeSlotRepository(new ApplicationContext());
            AppointmentRepository appointmentRepository = new AppointmentRepository(new ApplicationContext());
            IEnumerable<TimeSlot> allTimeSlots = timeSlotRepository.GetAll();
            List<TimeSlot> timeSlotsInTimeSlotPeriod = allTimeSlots.Where(timeSlot =>  timeSlot.StartTime >= timeSlotPeriod.StartPeriod)
                                                                    .Where(timeSlot => timeSlot.EndTime <= timeSlotPeriod.EndPeriod)
                                                                    .ToList();
            
            foreach(TimeSlot timeSlot in timeSlotsInTimeSlotPeriod)
            {
                timeSlot.IsAvailable = false;
                timeSlotRepository.Update(timeSlot);
                Appointment appointment = new Appointment(AppointmentType.AdminAppointment, timeSlot.TimeSlotId);
                appointmentRepository.Create(appointment);
            }
            EnableTime2 = false;
        }

        private DateTime ParseStringToDate(string dateTime)
        {
            DateTime dateTimeParsed = DateTime.Parse(dateTime);
            return dateTimeParsed;
        }
        private TimeSlot GetSelectectedTimeSlot()
        {
            TimeSlot timeSlot = new TimeSlot(_startDateAndTime, _endDateAndTime, false);
            return timeSlot;
        }
        private void ResetValues()
        {
            SetComboBoxIndex = "0";
            EnableTime1 = false;
            EnableTime2 = false;
            SelectedStartDateAndTime = "";
            SelectedEndDateAndTime = "";
        }

        private void CheckToken()
        {
            if (!new AuthenticationProvider().ValidateToken())
            {
                ChangeView?.Invoke(this, new ChangeViewEventArgs(nameof(AdminDashboardLoginViewModel)));
            }
        }
    }
}
