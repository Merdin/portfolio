using PitchPerfectHearingTest.Commands;
using PitchPerfectHearingTest.Interfaces;
using PitchPerfectHearingTest.Models;
using System;
using System.Windows.Input;
using PitchPerfectHearingTest.Dialogs.DialogService;
using PitchPerfectHearingTest.Services;

namespace PitchPerfectHearingTest.ViewModels
{
    public class IntroductionViewModel : BaseViewModel
    {
        private IDialogService<DialogWindow, IntroductionDialogViewModel> _dialogService;

        public ICommand StartTestSwitchViewCommand { get; set; }

        public ICommand ShowIntroductionDialogCommand { get; set; }

        public event EventHandler<ChangeViewEventArgs> ChangeView;

        public IntroductionViewModel()
        {
            _dialogService = new WindowDialogService<DialogWindow, IntroductionDialogViewModel>();
            ShowIntroductionDialogCommand = new RelayCommand(p => ShowDialog());
        }

        private void ShowDialog()
        {
            var result = _dialogService.ShowDialog("Test", new DialogWindow(), new IntroductionDialogViewModel());
            ChangeView?.Invoke(this, new ChangeViewEventArgs(nameof(HearingTestCountdownViewModel)));
        }
    }
}
