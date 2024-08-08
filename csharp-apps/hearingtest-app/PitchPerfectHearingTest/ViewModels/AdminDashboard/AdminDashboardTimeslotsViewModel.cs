using PitchPerfectHearingTest.Commands;
using PitchPerfectHearingTest.DataAccess;
using PitchPerfectHearingTest.DataAccess.Repositories;
using PitchPerfectHearingTest.Dialogs.DialogService;
using PitchPerfectHearingTest.Interfaces;
using PitchPerfectHearingTest.Models;
using PitchPerfectHearingTest.Services;
using PitchPerfectHearingTest.ViewModels.AdminDashboard;
using PitchPerfectHearingTest.ViewModels.AdminDashboard.Dialogs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Input;

namespace PitchPerfectHearingTest.ViewModels
{
    public class AdminDashboardTimeslotsViewModel : BaseViewModel
    {

        // Two dimensional List, the arrays in the List below are Lists with the appointments. Each List contains the appointments of one specific day.
        private Dictionary<String, List<Appointment>> _weekPlanning;
        private IDialogService<DialogWindow, AppointmentDialogMainViewModel> _appointmentDialogService;
        private AppointmentRepository _appointmentRepository = new AppointmentRepository(new ApplicationContext());
        private DateTime _filterStartDate;
        private DateTime _filterEndDate;
        private DateTime _selectedDate;

        public ICommand OpenAppointmentDialogCommand { get; set; }
        public ICommand ChangeFilterDate { get; set; }

        public DateTime SelectedDate
        {
            get { return _selectedDate; }
            set
            {
                if (value != null)
                {
                    _selectedDate = value;

                    _filterStartDate = value.StartOfWeek(DayOfWeek.Monday);
                    _filterEndDate = _filterStartDate.AddDays(6);

                    // Get appointments from Database & sort them
                    var allAppointments = _appointmentRepository.GetWeek(_filterStartDate, _filterEndDate);
                    WeekPlanning = SortAppointments(allAppointments);
                }

                base.OnPropertyChanged("SelectedDate");
                base.OnPropertyChanged("DateSelectionString");
            }
        }
        
        public string DateSelectionString
        {
            get { return $"{_filterStartDate.DayOfWeek} {_filterStartDate.Day}/{_filterStartDate.Month} - {_filterEndDate.DayOfWeek} {_filterEndDate.Day}/{_filterEndDate.Month}"; }
        }

        public Dictionary<String, List<Appointment>> WeekPlanning
        {
            get { return _weekPlanning; }
            set
            {
                if (value != null)
                {
                    _weekPlanning = value;
                }

                base.OnPropertyChanged("WeekPlanning");
            }
        }

        public event EventHandler<ChangeViewEventArgs> ChangeView;

        public AdminDashboardTimeslotsViewModel()
        {
            OpenAppointmentDialogCommand = new RelayCommand(Appointment => ShowDialog(Appointment));
            _appointmentDialogService = new WindowDialogService<DialogWindow, AppointmentDialogMainViewModel>();

            SelectedDate = DateTime.Now;

            // Set up appointment repository & get all appointments for current week
            var allAppointments = _appointmentRepository.GetWeek(_filterStartDate, _filterEndDate);
            WeekPlanning = SortAppointments(allAppointments);
        }

        private void ShowDialog(object app)
        {
            var appointment = app as Appointment;
            if (app == null) return;
            var windowname = $"{appointment.SelectedTimeSlot.StartTime.DayOfWeek} {appointment.SelectedTimeSlot.StartTime.Day}/{appointment.SelectedTimeSlot.StartTime.Month} | {appointment.SelectedTimeSlot.StartTime.Hour} - {appointment.SelectedTimeSlot.EndTime.Hour}";
            _appointmentDialogService.ShowDialog(windowname, new DialogWindow(), new AppointmentDialogMainViewModel(appointment));
            var allAppointments = _appointmentRepository.GetWeek(_filterStartDate, _filterEndDate);
            WeekPlanning = SortAppointments(allAppointments);
        }

        //
        // Summary:
        //     Converts appointments from a specific week to a dictionary with the following structure: String (Day & date), List (List with that days appointments)
        public Dictionary<String, List<Appointment>> SortAppointments(IEnumerable<Appointment> allAppointments)
        {
            var weekdays = new List<DayOfWeek>() { DayOfWeek.Monday, DayOfWeek.Tuesday, DayOfWeek.Wednesday, DayOfWeek.Thursday, DayOfWeek.Friday, DayOfWeek.Saturday, DayOfWeek.Sunday };
            var weekPlanning = new Dictionary<String, List<Appointment>>();

            weekdays.ForEach(day => {
                var dayStartTimes = (from appointment
                                     in allAppointments
                                     where appointment.SelectedTimeSlot.StartTime.DayOfWeek == day
                                     select appointment.SelectedTimeSlot.StartTime).ToList<DateTime>();
                
                // If there are no start times, there are no appointments for that day
                if (dayStartTimes.Count > 0)
                {
                    DateTime dayDate = dayStartTimes[0];
                    List<Appointment> dayAppointmens = (from appointment in allAppointments where appointment.SelectedTimeSlot.StartTime.DayOfWeek == day select appointment).ToList<Appointment>();

                    weekPlanning.Add($"{dayDate.DayOfWeek} {dayDate.Day}/{dayDate.Month}", dayAppointmens);
                }
            });

            return weekPlanning;
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