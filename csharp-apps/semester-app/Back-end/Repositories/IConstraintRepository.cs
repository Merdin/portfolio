using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Library.Models;

namespace Backend.Repositories
{
    public interface IConstraintRepository : IRepositoryBase<Constraint>
    {
        Task<Constraint> GetById(int Id);
    }
}
