using Microsoft.EntityFrameworkCore;
using PitchPerfectHearingTest.Interfaces;
using PitchPerfectHearingTest.Models;
using System.Collections.Generic;
using System.Linq;

namespace PitchPerfectHearingTest.DataAccess.Repositories
{
    public class HearingTestResultRepository : IRepository<HearingTestResult>
    {
        private readonly DbContext _context;

        public HearingTestResultRepository(ApplicationContext context)
        {
            _context = context;
        }

        public void Create(HearingTestResult entity)
        {
            _context.Add(entity);
            _context.SaveChanges();
        }

        public void Delete(HearingTestResult entity)
        {
            throw new System.NotImplementedException();
        }

        public IEnumerable<HearingTestResult> GetAll()
        {
            return _context.Set<HearingTestResult>().ToList();
        }

        public HearingTestResult GetSingle(int id)
        {
            return _context.Set<HearingTestResult>().Find(id);
        }

        public void Update(HearingTestResult entity)
        {
            _context.Update(entity);
            _context.SaveChanges();
        }
    }
}