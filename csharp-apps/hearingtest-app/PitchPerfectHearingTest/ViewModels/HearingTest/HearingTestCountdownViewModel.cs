using PitchPerfectHearingTest.Commands;
using System;
using System.Windows.Input;
using System.Windows.Threading;

namespace PitchPerfectHearingTest.ViewModels
{
    public class HearingTestCountdownViewModel:BaseViewModel
    {
        //Eventhandler switching to actual testview on zero count
        public delegate void CountDownHandler(object sender, EventArgs e);
        public event CountDownHandler CountDownToZero;

        //EventHandler reset countdown
        public event EventHandler<string> ResetOrStopEvent;
        public ICommand IntroductionViewSwitch { get; set; }
        public RelayCommand StartTimer { get;private set; }
        public RelayCommand StopTimer { get; private set; }
        
        private DispatcherTimer _timer;
        private int _countdown = 5;
        private string _time;
        
        private string _hideText;
        public string Time
        {
            get
            {
                return _countdown.ToString();
            }
            set
            {
                _time = value;
                base.OnPropertyChanged("Time");
            }

        }
    
        public HearingTestCountdownViewModel()
        {
            //Switch to testview
            CountDownToZero += (o, i) => HearingTestViewModel.GetInstance().SwitchView(nameof(HearingTestInteractionViewModel));

            StartCountDown();

        }


        /// <summary>
        /// Method to start countdown timer
        /// </summary>
        private void StartCountDown()
        {
            _timer = new DispatcherTimer();
            _timer.Interval = TimeSpan.FromSeconds(1);
            _timer.Tick += Timer_Tick;
            _timer.Start();
        }

        /// <summary>
        /// Subriber method to display countdown number string in textblock
        /// </summary>
        private void Timer_Tick(object sender, EventArgs e)
        {
            if (_countdown > 0)
            {
                _countdown--;
                Time = _countdown.ToString();            }
            else
            {
                CountDownToZero?.Invoke(this, EventArgs.Empty);
                _timer.Stop();
            }
        }
    }
}
