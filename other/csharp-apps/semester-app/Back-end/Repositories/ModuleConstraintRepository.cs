using System;
using System.Threading.Tasks;
using Backend.Data;
using Library.Models;
using Microsoft.EntityFrameworkCore;

namespace Backend.Repositories
{
    public class ModuleConstraintRepository : RepositoryBase<ModuleConstraint>, IModuleConstraintRepository
    {
        public ModuleConstraintRepository(DataContext dataContext) : base(dataContext)
        {
        }

        public async Task<bool> DeleteByModuleId(int moduleId)
        {
            var moduleConstraints = await FindByCondition(mc => mc.ModuleId.Equals(moduleId)).ToListAsync();
            foreach (var moduleConstraint in moduleConstraints)
            {
                Delete(moduleConstraint);
            }

            return true;
        }
    }
}
