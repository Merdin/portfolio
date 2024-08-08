using PitchPerfectHearingTest.Models;

namespace PitchPerfectHearingTest.ViewModels
{
    public class AdminDashboardViewModel : BaseViewModel
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

        //Private constructor to prevent instantiating of new objects
        public AdminDashboardViewModel()
        {
            SwitchView(nameof(AdminDashboardNavigationViewModel));
        }

        public void SwitchView(string view)
        {
            if (!view.Equals(nameof(AdminDashboardPasswordForgottenViewModel)) && !new AuthenticationProvider().ValidateToken())
            {
                view = nameof(AdminDashboardLoginViewModel);
            }

            switch (view)
            {
                case nameof(AdminDashboardLoginViewModel):
                    SelectedViewModel = new AdminDashboardLoginViewModel();
                    ((AdminDashboardLoginViewModel)SelectedViewModel).ChangeView += OnViewChanged;
                    break;
                case nameof(AdminDashboardPasswordForgottenViewModel):
                    SelectedViewModel = new AdminDashboardPasswordForgottenViewModel();
                    break;
                case nameof(AdminDashboardTimeslotsViewModel):
                    SelectedViewModel = new AdminDashboardTimeslotsViewModel();
                    ((AdminDashboardTimeslotsViewModel)SelectedViewModel).ChangeView += OnViewChanged;
                    break;
                case nameof(AdminDashboardDataAnalyseViewModel):
                    SelectedViewModel = new AdminDashboardDataAnalyseViewModel();
                    ((AdminDashboardDataAnalyseViewModel)SelectedViewModel).ChangeView += OnViewChanged;
                    break;
                case nameof(AdminDashboardTimeslotLockViewModel):
                    SelectedViewModel = new AdminDashboardTimeslotLockViewModel();
                    ((AdminDashboardTimeslotLockViewModel)SelectedViewModel).ChangeView += OnViewChanged;
                    break;
                default:
                    SelectedViewModel = new AdminDashboardNavigationViewModel();
                    ((AdminDashboardNavigationViewModel)SelectedViewModel).ChangeView += OnViewChanged;
                    break;
            }
        }

        private void OnViewChanged(object sender, ChangeViewEventArgs e)
        {
            SwitchView(e.View);
        }
    }
}