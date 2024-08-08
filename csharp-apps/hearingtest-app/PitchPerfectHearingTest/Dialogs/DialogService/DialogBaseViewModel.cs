using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;

namespace PitchPerfectHearingTest.Dialogs.DialogService
{
    public abstract class DialogBaseViewModel
    {
        public DialogResult UserDialogResult
        {
            get;
            private set;
        }
        public void CloseDialogWithResult(Window dialog, DialogResult result)
        {
            this.UserDialogResult = result;
            if (dialog != null)
                dialog.DialogResult = true;
        }

        public string Message
        {
            get;
            private set;
        }

        public DialogBaseViewModel(string message)
        {
            Message = message;
        }

    }
}
