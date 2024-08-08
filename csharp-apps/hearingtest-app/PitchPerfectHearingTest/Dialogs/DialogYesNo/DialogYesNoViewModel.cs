using PitchPerfectHearingTest.Commands;
using PitchPerfectHearingTest.Dialogs.DialogService;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Input;

namespace PitchPerfectHearingTest.Dialogs.DialogYesNo
{
    public class DialogYesNoViewModel : DialogBaseViewModel
    {
        //Commands to process Yes and No buttons

        private ICommand _yesCommand ;
        public ICommand YesCommand
        {
            get { return _yesCommand; }
            set { _yesCommand = value; }
        }

        private ICommand _noCommand;
        public ICommand NoCommand
        {
            get { return _noCommand; }
            set { _noCommand = value; }
        }

        public DialogYesNoViewModel(string message) : base(message)
        {
            _yesCommand = new RelayCommand(OnYesClicked);
            _noCommand = new RelayCommand(OnNoClicked);
        }

        private void OnNoClicked(object param)
        {
            //CloseDialogWithResult(param as DialogWindow, DialogResult.No);
            base.CloseDialogWithResult(param as DialogWindowMain, DialogResult.No);
        }

        private void OnYesClicked(object param)
        {
            CloseDialogWithResult(param as DialogWindowMain, DialogResult.Yes);


        }
    }
}
