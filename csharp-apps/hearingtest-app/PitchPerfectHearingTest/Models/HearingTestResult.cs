using System;
using System.Windows;
using System.Windows.Media;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;

namespace PitchPerfectHearingTest.Models
{
    [Table("HearingTestResults")]
    public class HearingTestResult : Entity
    {
        private List<SoundLevel> _scores = new List<SoundLevel>();
        [Key]
        public int HearingTestResultId { get; set; }
        public string Scores { get; set; }
        public Customer SelectedCustomer { get; set; }
        [ForeignKey("SelectedCustomer")]
        public int? Customer { get; set; }

        public HearingTestResult()
        {

        }

        public HearingTestResult(int hearingTestId, int customerId)
        {
            HearingTestResultId = hearingTestId;
            Customer = customerId;
        }

        public List<SoundLevel> GetScores()
        {
            return _scores;
        }

        public void AddSoundLevel(SoundLevel soundLevel)
        {
            bool soundLevelInScore = false;
            foreach(SoundLevel score in _scores)
            {
                if (score.Frequency == soundLevel.Frequency)
                {
                    soundLevelInScore = true;

                    if (score.Decibel > soundLevel.Decibel)
                    {
                        _scores.Add(soundLevel);
                        return;
                    }
                }
            }   

            if (!soundLevelInScore)
            {
                _scores.Add(soundLevel);
            }
        } 

        public void Clear()
        {
            _scores.Clear();
        }
    }
}