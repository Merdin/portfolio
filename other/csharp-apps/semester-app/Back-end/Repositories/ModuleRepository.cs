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
    public class ModuleRepository : RepositoryBase<Module>, IModuleRepository
    {
        public ModuleRepository(DataContext dataContext) : base(dataContext)
        {
        }

        public new async Task<List<Module>> GetAll() => await FindAll()
            .Include(module => module.ModuleConstraints)
            .ThenInclude(mc => mc.Constraint)
            .ToListAsync();

        public async Task<Module> GetById(int Id) => await FindByCondition(module => module.Id.Equals(Id))
            .Include(module => module.ModuleConstraints)
            .ThenInclude(mc => mc.Constraint)
            .FirstOrDefaultAsync();

        public async Task<List<Module>> GetAllActive() =>
            await FindByCondition(module => module.Status == ModuleStatus.Checked && module.Visible)
                .Include(module => module.ModuleConstraints)
                .ThenInclude(mc => mc.Constraint)
                .ToListAsync();
    }
}