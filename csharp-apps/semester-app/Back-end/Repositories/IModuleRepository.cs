using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Library.Models;

namespace Backend.Repositories
{
    public interface IModuleRepository : IRepositoryBase<Module>
    {
        new Task<List<Module>> GetAll();
        Task<Module> GetById(int Id);
        Task<List<Module>> GetAllActive();

    }
}
