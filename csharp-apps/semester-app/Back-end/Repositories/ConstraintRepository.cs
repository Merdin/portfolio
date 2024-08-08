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
    public class ConstraintRepository : RepositoryBase<Constraint>, IConstraintRepository
    {
        public ConstraintRepository(DataContext dataContext) : base(dataContext)
        {
        }
        
        public async Task<Constraint> GetById(int Id) => await FindByCondition(constraint => constraint.Id.Equals(Id))
            .Include(constraint => constraint.ModuleConstraints)
            .ThenInclude(mc => mc.Module)
            .FirstOrDefaultAsync();
    }
}
