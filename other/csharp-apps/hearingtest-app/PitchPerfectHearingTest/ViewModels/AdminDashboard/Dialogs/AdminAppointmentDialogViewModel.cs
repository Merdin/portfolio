using PitchPerfectHearingTest.Commands;
using PitchPerfectHearingTest.DataAccess;
using PitchPerfectHearingTest.DataAccess.Repositories;
using PitchPerfectHearingTest.Models;
using System.Windows.Input;

namespace PitchPerfectHearingTest.ViewModels.AdminDashboard
{
    public class AdminAppointmentDialogViewModel : BaseViewModel
    {
        private Appointment _appointment;
        private string _dateString;
        private string _timeString;

        public ICommand DeleteAppointmentCommand { get; set; }

        public string DateString
        {
            get { return _dateString; }
            set
            {
                if (value != null)
                {
                    _dateString = value;
                }

                base.OnPropertyChanged("DateString");
            }
        }

        public string TimeString
        {
            get { return _timeString; }
            set
            {
                if (value != null)
                {
                    _timeString = value;
                }

                base.OnPropertyChanged("TimeString");
            }
        }

        public AdminAppointmentDialogViewModel(Appointment appointment)
        {
            _appointment = appointment;
            DateString = $"{appointment.SelectedTimeSlot.StartTime.DayOfWeek} {appointment.SelectedTimeSlot.StartTime.Day}/{appointment.SelectedTimeSlot.StartTime.Month}";
            TimeString = $"{appointment.SelectedTimeSlot.StartTime.Hour} - {appointment.SelectedTimeSlot.EndTime.Hour}";

            DeleteAppointmentCommand = new RelayCommand(p => DeleteAppointment());
        }

        private void DeleteAppointment()
        {
            var appointmentRepo = new AppointmentRepository(new ApplicationContext());
            appointmentRepo.Delete(_appointment);
            DateString = "Deze afspraak is verwijderd. U kunt dit scherm nu sluiten.";
            TimeString = "";
        }
    }
}
