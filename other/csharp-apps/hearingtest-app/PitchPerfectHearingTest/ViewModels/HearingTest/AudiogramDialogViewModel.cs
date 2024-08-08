using PitchPerfectHearingTest.Models;
using PitchPerfectHearingTest.Services;
using System.Collections.Generic;
using System.Windows.Media;

namespace PitchPerfectHearingTest.ViewModels
{
    public class AudiogramDialogViewModel : BaseDialogViewModel
    {
        private PointCollection _resultMapping;
        private List<SoundLevel> _testScores;

        public PointCollection ResultMapping
        {
            get { return _resultMapping; }
            set
            {
                _resultMapping = value;
                base.OnPropertyChanged(nameof(ResultMapping));
            }
        }

        public AudiogramDialogViewModel(List<SoundLevel> scores)
        {
            _testScores = scores;
            MapResult();
        }

        private void MapResult()
        {
            ResultMapping = new AudiogramService().MapScore(_testScores);
        }
    }
}