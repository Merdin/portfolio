using System;

namespace PitchPerfectHearingTest.Models
{
    public class TimeSlotPeriod
    {
        public DateTime StartPeriod { get; set; }
        public DateTime EndPeriod { get; set; }

        public TimeSlotPeriod(DateTime startPeriod, DateTime endPeriod)
        {
            StartPeriod = startPeriod;
            EndPeriod = endPeriod;
        }
    }
}