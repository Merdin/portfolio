using System;
using System.Collections.Generic;
using System.Text;

namespace PitchPerfectHearingTest.Models
{
    public class ChangeViewEventArgs : EventArgs
    {
        public string View { get; set; }

        public ChangeViewEventArgs(string view)
        {
            View = view;
        }
    }
}
