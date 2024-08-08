using PitchPerfectHearingTest.Commands;
using PitchPerfectHearingTest.Models;
using System.Windows;
using System.Windows.Input;

namespace PitchPerfectHearingTest.ViewModels
{
    class MainWindowViewModel : BaseViewModel
    {
        private BaseViewModel _selectedViewModel;
        private HearingTestViewModel _hearingTestViewModel;

        public ICommand GoToHome { get; set; }
        public ICommand GoToAdmin { get; set; }

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

        public MainWindowViewModel()
        {
            // Get viewmodel instances
            _hearingTestViewModel = HearingTestViewModel.GetInstance();

            // Set the initial viewmodel
            _selectedViewModel = _hearingTestViewModel;

            // Create Commands

            /**
             * The GoToHome Command should set the MainWindow to show the hearing test & reset the hearing test itself to the first screen as well
             * Authenticated User will logout.
             */
            GoToHome = new ChangeViewCommand(p =>
            {
                _hearingTestViewModel.SwitchView(nameof(HearingTestIntroductionViewModel));
                SwitchView(nameof(HearingTestViewModel));
                Application.Current.Properties.Remove("AuthenticatedUser");
                Application.Current.Properties.Remove("SessionToken");
            });

            GoToAdmin = new ChangeViewCommand(p =>
            {
                SwitchView(nameof(AdminDashboardViewModel));                
            });
        }
        
        public void SwitchView(string view)
        {
            switch (view)
            {
                case nameof(HearingTestViewModel):
                    _hearingTestViewModel.SwitchView(nameof(IntroductionViewModel)); // Hier wellicht nog het hearing test object cleanen?
                    SelectedViewModel = _hearingTestViewModel;
                    break;
                case nameof(AdminDashboardViewModel):
                    SelectedViewModel = new AdminDashboardViewModel();
                    break;
            }
        }
    }
}
