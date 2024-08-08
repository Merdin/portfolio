using PitchPerfectHearingTest.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace PitchPerfectHearingTest.ViewModels
{
    public class IntroductionDialogViewModel : BaseDialogViewModel
    {

        private BaseViewModel _selectedViewModel;
        public BaseViewModel SelectedViewModel
        {
            get { return _selectedViewModel; }
            set
            {
                if (value != null)
                {
                    _selectedViewModel = value;
                }
                base.OnPropertyChanged(nameof(SelectedViewModel));
            }
        }

        public event EventHandler CloseRequest;
        public IntroductionDialogViewModel()
        {
            SwitchView(nameof(IntroductionDialog1Of4ViewModel));
        }

        public void SwitchView(string view)
        {
            switch (view)
            {
                case nameof(IntroductionDialogViewModel):
                case nameof(IntroductionDialog1Of4ViewModel):
                    BaseViewModel viewModel = new IntroductionDialog1Of4ViewModel();
                    ((IntroductionDialog1Of4ViewModel)viewModel).ViewChangeRequest += OnViewChanged;
                    SelectedViewModel = viewModel;
                    break;
                case nameof(IntroductionDialog2Of4ViewModel):
                    BaseViewModel viewModel2 = new IntroductionDialog2Of4ViewModel();
                    ((IntroductionDialog2Of4ViewModel)viewModel2).ViewChangeRequest += OnViewChanged;
                    SelectedViewModel = viewModel2;
                    break;
                case nameof(IntroductionDialog3Of4ViewModel):
                    BaseViewModel viewModel3 = new IntroductionDialog3Of4ViewModel();
                    ((IntroductionDialog3Of4ViewModel)viewModel3).ViewChangeRequest += OnViewChanged;
                    SelectedViewModel = viewModel3;
                    break;
                case nameof(IntroductionDialog4Of4ViewModel):
                    viewModel = new IntroductionDialog4Of4ViewModel();
                    ((IntroductionDialog4Of4ViewModel)viewModel).ReturnToHearingTestRequest += OnCloseRequest;
                    SelectedViewModel = viewModel;
                    break;
            }
        }

        protected virtual void OnViewChanged(object sender, ChangeViewEventArgs e)
        {
            SwitchView(e.View);
        }

        protected virtual void OnCloseRequest(object sender, EventArgs e)
        {
            base.OnRequestClose();
        }
    }
}
