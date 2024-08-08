using System.Collections.Generic;
using System.Threading.Tasks;
using Library.Models;

namespace Backend.Repositories
{
    public interface IModuleConstraintRepository : IRepositoryBase<ModuleConstraint>
    {
        Task<bool> DeleteByModuleId(int moduleId);
    }
}
