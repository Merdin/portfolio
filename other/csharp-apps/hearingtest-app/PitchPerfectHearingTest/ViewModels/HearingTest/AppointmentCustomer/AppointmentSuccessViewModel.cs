using PitchPerfectHearingTest.Commands;
using PitchPerfectHearingTest.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;

namespace PitchPerfectHearingTest.ViewModels
{
    public class AppointmentSuccessViewModel:BaseDialogViewModel
    {
        public event EventHandler ReturnToHearingTestRequest;

        public ICommand BackToResultPage { get; set; }

        public AppointmentSuccessViewModel()
        {
            BackToResultPage = new RelayCommand(p => ReturnToHearingTestResult());
        }


        private void ReturnToHearingTestResult()
        {
            ReturnToHearingTestRequest?.Invoke(this, new EventArgs()); 
        }
    }
}
