using Newtonsoft.Json;
using PitchPerfectHearingTest.Commands;
using PitchPerfectHearingTest.DataAccess;
using PitchPerfectHearingTest.DataAccess.Repositories;
using PitchPerfectHearingTest.Models;
using PitchPerfectHearingTest.Services;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Input;
using System.Windows.Media;

namespace PitchPerfectHearingTest.ViewModels.AdminDashboard
{
    public class CustAppointmentDialogViewModel : BaseViewModel
    {
        private Appointment _appointment;
        private string _dateString;
        private string _timeString;
        private string _customerName;
        private string _customerEmail;
        private PointCollection _resultMapping;

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

        public string CustomerName
        {
            get { return $"Naam: {_customerName}"; }
            set
            {
                if (value != null)
                {
                    _customerName = value;
                }

                base.OnPropertyChanged("CustomerName");
            }
        }

        public string CustomerEmail
        {
            get { return $"Email: {_customerEmail}"; }
            set
            {
                if (value != null)
                {
                    _customerEmail = value;
                }

                base.OnPropertyChanged("CustomerEmail");
            }
        }

        public PointCollection ResultMapping
        {
            get { return _resultMapping; }
            set
            {
                _resultMapping = value;
                base.OnPropertyChanged(nameof(ResultMapping));
            }
        }

        public CustAppointmentDialogViewModel(Appointment appointment)
        {
            _appointment = appointment;
            DateString = $"{appointment.SelectedTimeSlot.StartTime.DayOfWeek} {appointment.SelectedTimeSlot.StartTime.Day}/{appointment.SelectedTimeSlot.StartTime.Month}";
            TimeString = $"{appointment.SelectedTimeSlot.StartTime.Hour} - {appointment.SelectedTimeSlot.EndTime.Hour}";
            if (appointment.SelectedTestResult.SelectedCustomer != null)
            {
                CustomerName = $"{appointment.SelectedTestResult.SelectedCustomer.Name} ({appointment.SelectedTestResult.SelectedCustomer.Age})";
                CustomerEmail = appointment.SelectedTestResult.SelectedCustomer.Email;
            }
            DeleteAppointmentCommand = new RelayCommand(p => DeleteAppointment());

            MapResult(appointment.SelectedTestResult);

        }

        private void DeleteAppointment()
        {
            var appointmentRepo = new AppointmentRepository(new ApplicationContext());
            appointmentRepo.Delete(_appointment);
            DateString = "Deze afspraak is verwijderd. U kunt dit scherm nu sluiten.";
            TimeString = "";
        }

        private void MapResult(HearingTestResult result)
        {
            var scores = JsonConvert.DeserializeObject<List<SoundLevel>>(result.Scores, new JsonSerializerSettings() { TypeNameHandling = TypeNameHandling.All });
            if (scores != null && scores.Any())
            {
                ResultMapping = new AudiogramService().MapScore(scores);
            }
        }
    }
}