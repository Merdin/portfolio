using PitchPerfectHearingTest.Commands;
using PitchPerfectHearingTest.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;

namespace PitchPerfectHearingTest.ViewModels
{
    public class IntroductionDialog3Of4ViewModel : BaseDialogViewModel
    {
        public ICommand SwitchToDialog4Of4Command { get; set; }

        public event EventHandler<ChangeViewEventArgs> ViewChangeRequest;

        public IntroductionDialog3Of4ViewModel()
        {
            SwitchToDialog4Of4Command = new RelayCommand(p => SwitchView());
        }

        private void SwitchView()
        {
            ViewChangeRequest?.Invoke(this, new ChangeViewEventArgs(nameof(IntroductionDialog4Of4ViewModel)));
        }
    }
}
