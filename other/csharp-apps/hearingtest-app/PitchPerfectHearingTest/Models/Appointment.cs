using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PitchPerfectHearingTest.Models
{
    [Table("Appointments")]
    public class Appointment : Entity
    {
        [Key]
        public int AppointmentId { get; set; }
        public AppointmentType Type { get; set; }
        public TimeSlot SelectedTimeSlot { get; set; }

        [ForeignKey("SelectedTimeSlot")]
        public int TimeSlot { get; set; }

        public HearingTestResult SelectedTestResult { get; set; }

        [ForeignKey("SelectedTestResult")]
        public int? HearingTestResult { get; set; }

        public Appointment() { }
        

        public Appointment(AppointmentType type, int timeSlotId, int selectedHearingTestResult)
        {
            Type = type;
            TimeSlot = timeSlotId;
            HearingTestResult = selectedHearingTestResult;
        }
        public Appointment(int id, AppointmentType type, TimeSlot timeSlot)
        {
            AppointmentId = id;
            Type = type;
            SelectedTimeSlot = timeSlot;
        }
        
        public Appointment(int id, AppointmentType type, int timeSlotId,int selectedHearingTestResult)
        {
            AppointmentId = id;
            Type = type;
            TimeSlot = timeSlotId;
            HearingTestResult = selectedHearingTestResult;

        }

        public Appointment(AppointmentType type, int timeSlotId)
        {
            Type = type;
            TimeSlot = timeSlotId;
        }
    }
}
