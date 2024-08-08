using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Backend.Data;
using Microsoft.EntityFrameworkCore;
using Library.Models;

namespace Backend.Repositories
{
    public class LearningUnitRepository : RepositoryBase<LearningUnit>, ILearningUnitRepository
    {
        public LearningUnitRepository(DataContext dataContext) : base(dataContext)
        {
        }

        public new async Task<List<LearningUnit>> GetAll() => await FindAll()
            .Include(s => s.LearningUnitModules)
            .ThenInclude(sm => sm.Module)
            .ThenInclude(m => m.ModuleConstraints)
            .ThenInclude(mc => mc.Constraint)
            .ToListAsync();

        public async Task<LearningUnit> GetById(int Id) => await FindByCondition(semester => semester.Id.Equals(Id))
            .Include(s => s.LearningUnitModules)
            .ThenInclude(sm => sm.Module)
            .ThenInclude(m => m.ModuleConstraints)
            .ThenInclude(mc => mc.Constraint)
            .FirstOrDefaultAsync();
    }
}
