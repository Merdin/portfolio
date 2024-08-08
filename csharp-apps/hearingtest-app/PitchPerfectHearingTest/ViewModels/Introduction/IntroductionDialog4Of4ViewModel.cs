using PitchPerfectHearingTest.Commands;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;

namespace PitchPerfectHearingTest.ViewModels
{
    public class IntroductionDialog4Of4ViewModel : BaseDialogViewModel
    {
        public ICommand SwitchToHearingTest { get; set; }

        public event EventHandler ReturnToHearingTestRequest;

        public IntroductionDialog4Of4ViewModel()
        {
            SwitchToHearingTest = new RelayCommand(p => ReturnToHearingTest());
        }

        private void ReturnToHearingTest()
        {
            ReturnToHearingTestRequest?.Invoke(this, new EventArgs());
        }
    }
}
