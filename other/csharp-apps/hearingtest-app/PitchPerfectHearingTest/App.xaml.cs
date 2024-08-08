using System;
using System.Linq;
using System.Windows;
using System.Globalization;
using System.Collections.Generic;
using PitchPerfectHearingTest.Models;
using PitchPerfectHearingTest.DataAccess;
using PitchPerfectHearingTest.DataAccess.Repositories;

namespace PitchPerfectHearingTest
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private const int DaysInFuture = 21;
        private const int MinutesPerTimeSlot = 60;
        private const int TimeSlotsPerDay = 6;
        private const string FirstTimeSlotTime = "10:30";

        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);
            
            TimeSlotRepository timeSlotRepository = new TimeSlotRepository(new ApplicationContext());
            IEnumerable<TimeSlot> timeSlots = timeSlotRepository.GetAll();
            DateTime latestTimeSlotDateTime = timeSlots.Max(x => x.EndTime);
            DateTime now = DateTime.Now;   
            DateTime currentDate = now.Date;

            for(int i = 0; i < DaysInFuture; i++)
            {
                DateTime indexDate = currentDate.AddDays(i);
                DayOfWeek day = indexDate.DayOfWeek;

                if ((DateTime.Compare(latestTimeSlotDateTime, indexDate) > 0) 
                    || !(day >= DayOfWeek.Monday) && (day <= DayOfWeek.Friday))
                {
                    continue;
                }  
                
                for(int j = 0; j < TimeSlotsPerDay; j++)
                {
                    TimeSpan time = DateTime.ParseExact(FirstTimeSlotTime, "HH:mm", CultureInfo.InvariantCulture).TimeOfDay;
                    DateTime startDateTime = indexDate + time;
                    startDateTime = startDateTime.AddMinutes(MinutesPerTimeSlot * j);
                    DateTime endDateTime = indexDate + time;
                    endDateTime = endDateTime.AddMinutes(MinutesPerTimeSlot * (j + 1));
                    TimeSlot timeSlot = new TimeSlot(startDateTime, endDateTime, true);
                    timeSlotRepository.Create(timeSlot);
                }
            }
        }
    }
}
