using System.Collections.Generic;
using System.Threading.Tasks;
using Backend.Data;
using Microsoft.EntityFrameworkCore;
using Library.Models;

namespace Backend.Repositories
{
    public class StudyPlanRepository : RepositoryBase<StudyPlan>, IStudyPlanRepository
    {
        public StudyPlanRepository(DataContext dataContext) : base(dataContext)
        {
        }

        public new async Task<List<StudyPlan>> GetAll() =>
            await FindAll().Include(studyPlan => studyPlan.User).ToListAsync();

        public async Task<StudyPlan> GetById(int id) =>
            await FindByCondition((StudyPlan studyPlan) => studyPlan.Id.Equals(id))
                .FirstOrDefaultAsync();

        public async Task<List<StudyPlan>> GetByUserId(int id) =>
            await FindByCondition((StudyPlan studyPlan) => studyPlan.User.Id.Equals(id))
                .ToListAsync();
    }
}