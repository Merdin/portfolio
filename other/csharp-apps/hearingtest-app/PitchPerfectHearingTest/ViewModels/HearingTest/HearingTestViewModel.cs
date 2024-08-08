using PitchPerfectHearingTest.Commands;
using PitchPerfectHearingTest.Dialogs;
using PitchPerfectHearingTest.Models;
using PitchPerfectHearingTest.Services;

namespace PitchPerfectHearingTest.ViewModels
{
    public class HearingTestViewModel : BaseViewModel
    {
        //Creating single instance of HearingTestViewModel (Singleton)
        private static HearingTestViewModel _instance;

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

        //Static method to get single instance of HearingTestViewModel
        public static HearingTestViewModel GetInstance()
        {
            if (_instance == null) _instance = new HearingTestViewModel();
            return _instance;
        }
        
        //Private constructor to prevent instantiating of new objects
        private HearingTestViewModel()
        {
            /**
             * In de introductie view komt er een start test knop (of dergelijks). Deze knop moet gekoppeld zitten aan een command 
             * in de introductie viewModel door middel van binding. Met onderstaande code can je die command ook echt wat laten doen.
             * In dit geval runt hij dus eigenlijk de SwitchView methode van DEZE klasse
             */
            //_introductionViewModel.StartTest = new ChangeViewCommand(p => SwitchView(nameof(HearingTestViewModel)));
            SwitchView(nameof(IntroductionViewModel));
        }

        public void SwitchView(string view)
        {
            switch (view)
            {
                case nameof(IntroductionViewModel):
                    SelectedViewModel = new IntroductionViewModel();
                    ((IntroductionViewModel)SelectedViewModel).ChangeView += OnChangeView;
                    break;
                case nameof(HearingTestCountdownViewModel):
                    SelectedViewModel = new HearingTestCountdownViewModel();
                    break;
                case nameof(HearingTestInteractionViewModel):
                    SelectedViewModel = new HearingTestInteractionViewModel();
                    break;
                case nameof(HearingTestResultViewModel):
                    SelectedViewModel = new HearingTestResultViewModel(new WindowDialogService<AudiogramDialog, AudiogramDialogViewModel>());
                    break;
            }
        }
        protected void OnChangeView(object sender, ChangeViewEventArgs e)
        {
            SwitchView(e.View);
        }
    }
}
