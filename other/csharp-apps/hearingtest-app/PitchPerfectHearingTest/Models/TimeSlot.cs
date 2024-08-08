using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PitchPerfectHearingTest.Models
{
    [Table("TimeSlots")]
    public class TimeSlot : Entity
    {
        [Key]
        public int TimeSlotId { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public bool IsAvailable { get; set; }

        public TimeSlot() { }

        public TimeSlot(DateTime startTime, DateTime endTime, bool isAvailable)
        {
            StartTime = startTime;
            EndTime = endTime;
            IsAvailable = isAvailable;
        }

        public TimeSlot(int id, DateTime startTime, DateTime endTime, bool isAvailable)
        {
            TimeSlotId = id;
            StartTime = startTime;
            EndTime = endTime;
            IsAvailable = isAvailable;
        }

        public override string ToString()
        {
            return
                $"Begin: {StartTime}\n" +
                $"Eind: {EndTime} ";
        }
    }
}