using Microsoft.EntityFrameworkCore;
using PitchPerfectHearingTest.Interfaces;
using PitchPerfectHearingTest.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace PitchPerfectHearingTest.DataAccess.Repositories
{
    public class TimeSlotRepository : IRepository<TimeSlot>
    {
        private readonly DbContext _context;

        public TimeSlotRepository(ApplicationContext context)
        {
            _context = context;
        }

        public void Create(TimeSlot entity)
        {
            _context.Set<TimeSlot>().Add(entity);
            _context.SaveChanges();
        }

        public void Delete(TimeSlot entity)
        {
            _context.Set<TimeSlot>().Remove(entity);
            _context.SaveChanges();
        }

        public IEnumerable<TimeSlot> GetAll()
        {
            return _context.Set<TimeSlot>().ToList();
        }

        public IEnumerable<TimeSlot> GetSelection(TimeSlot selectedDate)
        {
            //Get all timeslots
            IEnumerable<TimeSlot> list = (List<TimeSlot>)GetAll();
            //Get all timeslots from selected date
            var filteredList = from t in list where t.StartTime.Date == selectedDate.StartTime.Date select t;
            return filteredList.ToList();
        }


        public TimeSlot GetSingle(int id)
        {
            return _context.Set<TimeSlot>().Find(id);
        }
        
        

        public void Update(TimeSlot entity)
        {
            _context.Set<TimeSlot>().Update(entity);
            _context.SaveChanges();
        }
    }
}