using PitchPerfectHearingTest.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace PitchPerfectHearingTest.ViewModels.AdminDashboard.Dialogs
{
    public class AppointmentDialogMainViewModel : BaseDialogViewModel
    {

        private Appointment _appointment;
        private CustAppointmentDialogViewModel _custAppointmentDialogViewModel;
        private AdminAppointmentDialogViewModel _adminAppointmentDialogViewModel;
        private BaseViewModel _selectedViewModel;

        public BaseViewModel SelectedViewModel
        {
            get { return _selectedViewModel; }
            set
            {
                if (value != null)
                {
                    _selectedViewModel = value;
                }

                base.OnPropertyChanged(nameof(SelectedViewModel));
            }
        }

        public AppointmentDialogMainViewModel(Appointment appointment)
        {
            _appointment = appointment;

            if (_appointment.Type == AppointmentType.AdminAppointment)
            {
                _adminAppointmentDialogViewModel = new AdminAppointmentDialogViewModel(_appointment);
                SelectedViewModel = _adminAppointmentDialogViewModel;
            }
            else if (_appointment.Type == AppointmentType.CustomerAppointment)
            {
                _custAppointmentDialogViewModel = new CustAppointmentDialogViewModel(_appointment);
                SelectedViewModel = _custAppointmentDialogViewModel;
            }
        }
    }
}
