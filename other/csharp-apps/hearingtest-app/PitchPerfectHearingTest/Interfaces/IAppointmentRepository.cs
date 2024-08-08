using PitchPerfectHearingTest.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace PitchPerfectHearingTest.Interfaces
{
    public interface IAppointmentRepository : IRepository<Appointment>
    {
        public IEnumerable<Appointment> GetWeek(DateTime beginTime, DateTime endTime);
    }
}
