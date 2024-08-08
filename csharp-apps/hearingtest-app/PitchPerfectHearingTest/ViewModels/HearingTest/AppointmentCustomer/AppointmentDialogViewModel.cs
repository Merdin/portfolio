using PitchPerfectHearingTest.Dialogs.DialogYesNo;
using PitchPerfectHearingTest.Models;
using PitchPerfectHearingTest.ViewModels.AdminDashboard.Dialogs;
using System;
using System.Collections.Generic;
using System.Text;

namespace PitchPerfectHearingTest.ViewModels
{
    public class AppointmentDialogViewModel : BaseDialogViewModel
    {
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
        public AppointmentDialogViewModel()
        {
            SwitchView(nameof(AppointmentSelectTimeDialogViewModel));
        }

        public void SwitchView(string view)
        {
            switch(view)
            {
                case nameof(AppointmentDialogViewModel):
                case nameof(AppointmentSelectTimeDialogViewModel):
                    BaseViewModel viewModel1 = new AppointmentSelectTimeDialogViewModel();
                    ((AppointmentSelectTimeDialogViewModel)viewModel1).ViewChangeRequest += OnViewChanged;
                    SelectedViewModel = viewModel1;
                    break;
                case nameof(AppointmentAddContactInfoViewModel):
                    BaseViewModel viewModel2 = new AppointmentAddContactInfoViewModel();
                    ((AppointmentAddContactInfoViewModel)viewModel2).ViewChangeRequest += OnViewChanged;
                    SelectedViewModel = viewModel2;
                    break;
                case nameof(AppointmentSuccessViewModel):
                    BaseViewModel viewModel3 = new AppointmentSuccessViewModel();
                    ((AppointmentSuccessViewModel)viewModel3).ReturnToHearingTestRequest += OnCloseRequest;
                    SelectedViewModel = viewModel3;
                    break;
            }
        }

        protected virtual void OnViewChanged(object sender, ChangeViewEventArgs e)
        {
            SwitchView(e.View);
        }

        protected virtual void OnCloseRequest(object sender, EventArgs e)
        {
            base.OnRequestClose();
        }

    }
}
