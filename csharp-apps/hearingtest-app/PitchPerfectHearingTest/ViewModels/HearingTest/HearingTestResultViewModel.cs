using Newtonsoft.Json;
using PitchPerfectHearingTest.Commands;
using PitchPerfectHearingTest.DataAccess;
using PitchPerfectHearingTest.DataAccess.Repositories;
using PitchPerfectHearingTest.Dialogs;
using PitchPerfectHearingTest.Interfaces;
using PitchPerfectHearingTest.Models;
using PitchPerfectHearingTest.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Input;
using System.Windows.Media;

namespace PitchPerfectHearingTest.ViewModels
{
    public class HearingTestResultViewModel : BaseViewModel
    {
        private IDialogService<AudiogramDialog, AudiogramDialogViewModel> _audiogramDialogService;
        private WindowDialogService<AppointmentDialog, AppointmentDialogViewModel> _appointmentDialogService;
        private string _resultIndication;
        private Brush _indicationColour;
        private List<SoundLevel> _testScores;

        public ICommand ShowAudiogram { get; set; }
        public ICommand ShowAppointment { get; set; }

        public string ResultIndication
        {
            get { return _resultIndication ?? string.Empty; }
            set
            {
                if (!string.IsNullOrEmpty(value))
                {
                    _resultIndication = value;
                }

                base.OnPropertyChanged("ResultIndication");
            }
        }

        public Brush IndicationColour
        {
            get {return _indicationColour; }
            set
            {
                _indicationColour = value;
                base.OnPropertyChanged("IndicationColour");
            }
        }

        public HearingTestResultViewModel(IDialogService<AudiogramDialog, AudiogramDialogViewModel> audiogramDialogService)
        {
            GetTestResult();
            _audiogramDialogService = audiogramDialogService;
            _appointmentDialogService = new WindowDialogService<AppointmentDialog, AppointmentDialogViewModel>();
            ShowAudiogram = new RelayCommand(p => ShowAudiogramDialog());
            ShowAppointment = new RelayCommand(p => ShowAppointmentDialog());
        }

        private void ShowAudiogramDialog()
        {
            _audiogramDialogService.ShowDialog("Audiogram", new AudiogramDialog(), new AudiogramDialogViewModel(_testScores));
        }

        private void ShowAppointmentDialog()
        {
            _appointmentDialogService.ShowDialog("Afspraak", new AppointmentDialog(), new AppointmentDialogViewModel());
        }

        private void GetTestResult()
        {
            if (App.Current.Properties.Contains("TestResultId")) 
            {
                var testResultId = (int)App.Current.Properties["TestResultId"];
                try
                {
                    var result = new HearingTestResultRepository(new ApplicationContext()).GetSingle(testResultId);
                    _testScores = JsonConvert.DeserializeObject<List<SoundLevel>>(result.Scores);
                    DetermineIndication();
                }
                catch (Exception excep)
                {
                    throw;
                }
            }
        }

        private void DetermineIndication()
        {
            var worstScore = _testScores.OrderByDescending(s => s.Decibel)?.First();
            if (worstScore != null)
            {
                if ((int)worstScore.Decibel <= 20)
                {
                    _resultIndication = "Uw gehoor is goed, u hoeft geen afspraak te maken.";
                    _indicationColour = Brushes.Green;
                }
                else if ((int)worstScore.Decibel <= 30)
                {
                    _resultIndication = "U heeft mogelijk lichte gehoorschade, \neen afspraak maken is niet noodzakelijk.";
                    _indicationColour = Brushes.LightGreen;
                }
                else
                {
                    _resultIndication = "U heeft mogelijk gehoorschade, \nwij raden u aan een afspraak te maken!";
                    _indicationColour = Brushes.Red;
                }
            }
        }
    }
}