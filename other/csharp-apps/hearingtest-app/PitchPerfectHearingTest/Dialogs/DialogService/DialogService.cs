using PitchPerfectHearingTest.Dialogs.DialogYesNo;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;

namespace PitchPerfectHearingTest.Dialogs.DialogService
{
    public static class DialogService
    {
        public static DialogResult OpenDialog(DialogBaseViewModel vm, Window owner)
        {
            DialogWindowMain window = new DialogWindowMain();
            if (owner != null)
                window.Owner = owner;
            window.DataContext = vm;
            window.ShowDialog();

            //Set value to UserDialogResult property of hosting window datacontext
            DialogResult result = (window.DataContext as DialogYesNoViewModel).UserDialogResult;
            return result;
        }
    }
}
