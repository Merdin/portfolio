using PitchPerfectHearingTest.Commands;
using PitchPerfectHearingTest.Models;
using System;
using System.Windows;
using System.Windows.Input;
using System.Windows.Threading;

namespace PitchPerfectHearingTest.ViewModels
{
    public class HearingTestInteractionViewModel : BaseViewModel
    {
        private HearingTest _hearingTest;
        private Visibility _resultViewEnabled = Visibility.Hidden;

        public ICommand HeardSoundCommand { get; set; }
        public ICommand FinishHearingTest { get; set; }

        public Visibility ResultViewEnabled
        {
            get { return _resultViewEnabled; }
            set
            {
                _resultViewEnabled = value;
                base.OnPropertyChanged("ResultViewEnabled");
            }
        }

        public HearingTestInteractionViewModel()
        {
            _hearingTest = SessionContext.HearingTest;
            _hearingTest.TestEnd += OnTestEnded;
            HeardSoundCommand = new ExecuteEventCommand(p => _hearingTest.HeardSound());
            FinishHearingTest = new ChangeViewCommand(p => HearingTestViewModel.GetInstance().SwitchView(nameof(HearingTestResultViewModel)));

            Application.Current.Dispatcher.BeginInvoke(DispatcherPriority.Normal, new Action(() =>
            {
                _hearingTest.HandleStartTest();
            }));
        }

        private void OnTestEnded(object sender, EventArgs e)
        {
            ResultViewEnabled = Visibility.Visible;
        }

    }
}