using System;
using System.Collections.Generic;
using System.Text;

namespace PitchPerfectHearingTest.Models
{
    public class RequestCloseDialogEventArgs : EventArgs
    {
        public bool DialogResult { get; set; }

        public RequestCloseDialogEventArgs(bool dialogResult)
        {
            DialogResult = dialogResult;
        }
    }
}