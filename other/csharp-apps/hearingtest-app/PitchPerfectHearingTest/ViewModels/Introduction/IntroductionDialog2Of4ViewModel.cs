using PitchPerfectHearingTest.Commands;
using PitchPerfectHearingTest.Models;
using System;
using System.Windows.Input;

namespace PitchPerfectHearingTest.ViewModels
{
    public class IntroductionDialog2Of4ViewModel : BaseDialogViewModel
    {
        public ICommand SwitchToDialog3Of4Command { get; set; }

        public event EventHandler<ChangeViewEventArgs> ViewChangeRequest;

        public IntroductionDialog2Of4ViewModel()
        {
            SwitchToDialog3Of4Command = new RelayCommand(p => SwitchView());
        }

        private void SwitchView()
        {
            ViewChangeRequest?.Invoke(this, new ChangeViewEventArgs(nameof(IntroductionDialog3Of4ViewModel)));
        }
    }
}
