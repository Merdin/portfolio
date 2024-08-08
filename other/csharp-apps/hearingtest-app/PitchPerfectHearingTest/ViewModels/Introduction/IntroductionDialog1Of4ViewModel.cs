using PitchPerfectHearingTest.Commands;
using PitchPerfectHearingTest.Models;
using System;
using System.Windows.Input;

namespace PitchPerfectHearingTest.ViewModels
{
    public class IntroductionDialog1Of4ViewModel : BaseDialogViewModel
    {
        public ICommand SwitchToDialog2Of4Command { get; set; }

        public event EventHandler<ChangeViewEventArgs> ViewChangeRequest;

        public IntroductionDialog1Of4ViewModel()
        {
            SwitchToDialog2Of4Command = new RelayCommand(p => SwitchView());
        }

        private void SwitchView()
        {
            ViewChangeRequest?.Invoke(this, new ChangeViewEventArgs(nameof(IntroductionDialog2Of4ViewModel)));
        }
    }
}
