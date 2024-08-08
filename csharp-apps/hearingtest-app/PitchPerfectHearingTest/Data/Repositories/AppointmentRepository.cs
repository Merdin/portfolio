using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using PitchPerfectHearingTest.Interfaces;
using PitchPerfectHearingTest.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace PitchPerfectHearingTest.DataAccess.Repositories
{
    public class AppointmentRepository : IAppointmentRepository
    {
        private readonly DbContext _context;

        public AppointmentRepository()
        {
        }

        public AppointmentRepository(ApplicationContext context)
        {
            _context = context;
        }

        public void Create(Appointment entity)
        {
                _context.Add(entity);
                _context.SaveChanges();
        }

        public void Delete(Appointment entity)
        {
            _context.Set<Appointment>().Remove(entity);
            _context.SaveChanges();
        }

        public IEnumerable<Appointment> GetAll()
        {
            return _context.Set<Appointment>().ToList();

        }

        public Appointment GetSingle(int id)
        {
            var appointments = GetAll();
            var singleAppointment = ((Appointment)(from Appointment a in appointments
                                                   where a.AppointmentId == id
                                                   select a).First());
            return singleAppointment;

        }

        public void Update(Appointment entity)
        {
            throw new System.NotImplementedException();
        }
        
        public IEnumerable<Appointment> GetWeek(DateTime beginTime, DateTime endTime)
        {
            // Get all appointments
            var allAppointments = _context.Set<Appointment>().ToList();

            // Get the timeslots
            var timeSlotRepo = new TimeSlotRepository((ApplicationContext)_context);
            var allTimeSlots = timeSlotRepo.GetAll();

           
            // Get the test results
            var testResultRepo = new HearingTestResultRepository((ApplicationContext)_context);
            var allTestResults = testResultRepo.GetAll();

            // Get all customers
            var customerRepo = new CustomerRepository((ApplicationContext)_context);
            var allCustomers = customerRepo.GetAll();

            // Connect customers to appointments
            foreach(var testResult in allTestResults)
            {
                if (testResult.Customer != null)
                {
                    testResult.SelectedCustomer = allCustomers.Single(c => c.CustomerId == testResult.Customer);
                }
            }

            // Connect test results & timeslots to appointments
            foreach (var app in allAppointments)
            {
                app.SelectedTimeSlot = allTimeSlots.Single(t => t.TimeSlotId == app.TimeSlot);

                app.SelectedTestResult = allTestResults.FirstOrDefault(t => t.HearingTestResultId == app.HearingTestResult);
            }

            var result = allAppointments.Where(a => a.SelectedTimeSlot.StartTime.Ticks >= beginTime.Ticks && a.SelectedTimeSlot.StartTime.Ticks <= endTime.Ticks).ToList();
            return result;
        }
    }
}
