using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PitchPerfectHearingTest.DataAccess;
using PitchPerfectHearingTest.DataAccess.Repositories;
using PitchPerfectHearingTest.Models;

namespace PitchPerfectHearingTest.test.ViewModels.AdminDashboard.Dialogs
{
    [TestClass()]
    public class AppointmentDatabaseTest
    {
            
            [TestMethod()]
            public void ExecuteShouldCreateAppointment()
            {
                //Arrange
                var options = new DbContextOptionsBuilder<ApplicationContext>().UseInMemoryDatabase(databaseName: "ShouldCreateAppointment").Options;

                var context = new ApplicationContext(options);
                var appointmentRepo = new AppointmentRepository(context);

                var newAppointment = new Appointment(11, AppointmentType.CustomerAppointment, 3, 22);

                //Act
                appointmentRepo.Create(newAppointment);
                var savedAppointment = appointmentRepo.GetSingle(newAppointment.AppointmentId);

                //Assert
                Assert.AreEqual(11, savedAppointment.AppointmentId);
                Assert.AreEqual(AppointmentType.CustomerAppointment, savedAppointment.Type);
                Assert.AreEqual(3, savedAppointment.TimeSlot);
                Assert.AreEqual(22, savedAppointment.HearingTestResult);


            }
    }

}

