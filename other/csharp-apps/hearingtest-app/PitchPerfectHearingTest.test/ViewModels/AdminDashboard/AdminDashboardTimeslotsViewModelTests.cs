using Microsoft.VisualStudio.TestTools.UnitTesting;
using PitchPerfectHearingTest.Models;
using PitchPerfectHearingTest.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;

namespace PitchPerfectHearingTest.test.ViewModels.AdminDashboard
{
    [TestClass()]
    public class AdminDashboardTimeslotsViewModelTests
    {
        [TestMethod()]
        public void SortAppointmentsTest()
        {
            // Arrange
            var ADTVM = new AdminDashboardTimeslotsViewModel();

            var tempTimeSlot1 = new TimeSlot(1, new DateTime(2020, 11, 25, 8, 0, 0), new DateTime(2020, 11, 25, 9, 0, 0), true);
            var tempTimeSlot2 = new TimeSlot(2, new DateTime(2020, 11, 25, 9, 0, 0), new DateTime(2020, 11, 25, 10, 0, 0), true);
            var tempTimeSlot3 = new TimeSlot(3, new DateTime(2020, 11, 25, 10, 0, 0), new DateTime(2020, 11, 25, 11, 0, 0), true);
            var tempTimeSlot4 = new TimeSlot(4, new DateTime(2020, 11, 25, 11, 0, 0), new DateTime(2020, 11, 25, 12, 0, 0), true);
            var tempTimeSlot5 = new TimeSlot(5, new DateTime(2020, 11, 25, 12, 0, 0), new DateTime(2020, 11, 25, 13, 0, 0), true);
            var tempTimeSlot6 = new TimeSlot(6, new DateTime(2020, 11, 26, 8, 0, 0), new DateTime(2020, 11, 26, 9, 0, 0), true);
            var tempTimeSlot7 = new TimeSlot(7, new DateTime(2020, 11, 26, 9, 0, 0), new DateTime(2020, 11, 26, 10, 0, 0), true);
            var tempTimeSlot8 = new TimeSlot(8, new DateTime(2020, 11, 26, 10, 0, 0), new DateTime(2020, 11, 26, 11, 0, 0), true);
            var tempTimeSlot9 = new TimeSlot(9, new DateTime(2020, 11, 26, 11, 0, 0), new DateTime(2020, 11, 26, 12, 0, 0), true);
            var tempTimeSlot10 = new TimeSlot(10, new DateTime(2020, 11, 26, 12, 0, 0), new DateTime(2020, 11, 26, 13, 0, 0), true);

            var tempAppointment1 = new Appointment(1, AppointmentType.CustomerAppointment, tempTimeSlot1);
            var tempAppointment2 = new Appointment(2, AppointmentType.CustomerAppointment, tempTimeSlot2);
            var tempAppointment3 = new Appointment(3, AppointmentType.CustomerAppointment, tempTimeSlot3);
            var tempAppointment4 = new Appointment(4, AppointmentType.AdminAppointment, tempTimeSlot4);
            var tempAppointment5 = new Appointment(5, AppointmentType.CustomerAppointment, tempTimeSlot5);
            var tempAppointment6 = new Appointment(6, AppointmentType.CustomerAppointment, tempTimeSlot6);
            var tempAppointment7 = new Appointment(7, AppointmentType.CustomerAppointment, tempTimeSlot7);
            var tempAppointment8 = new Appointment(8, AppointmentType.CustomerAppointment, tempTimeSlot8);
            var tempAppointment9 = new Appointment(9, AppointmentType.AdminAppointment, tempTimeSlot9);
            var tempAppointment10 = new Appointment(10, AppointmentType.CustomerAppointment, tempTimeSlot10);

            List<Appointment> input = new List<Appointment>
            {
                tempAppointment1, tempAppointment2, tempAppointment3, tempAppointment4, tempAppointment5, tempAppointment6, tempAppointment7, tempAppointment8, tempAppointment9, tempAppointment10
            };

            Dictionary<string, List<Appointment>> expectedOutput = new Dictionary<string, List<Appointment>>
            {
                {"Wednesday 25/11", new List<Appointment> {tempAppointment1, tempAppointment2, tempAppointment3, tempAppointment4, tempAppointment5}},
                {"Thursday 26/11", new List<Appointment> {tempAppointment6, tempAppointment7, tempAppointment8, tempAppointment9, tempAppointment10}}
            };

            // Act
            Dictionary<string, List<Appointment>> sortedAppointments = ADTVM.SortAppointments(input);

            // Assert
            Assert.IsTrue(expectedOutput.Keys.All(k => sortedAppointments.ContainsKey(k)));
            Assert.IsTrue(sortedAppointments.Keys.All(k => expectedOutput.ContainsKey(k)));

            foreach (KeyValuePair<string, List<Appointment>> day in sortedAppointments)
            {
                var sortedDailyAppointments = day.Value;
                var expectedDailyAppointments = expectedOutput[day.Key.ToString()];
                CollectionAssert.AreEqual(sortedDailyAppointments, expectedDailyAppointments);
            }
        }
    }
}