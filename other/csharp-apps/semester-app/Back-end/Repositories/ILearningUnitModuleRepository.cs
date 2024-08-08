using System.Threading.Tasks;
using Library.Models;

namespace Backend.Repositories
{
    public interface ILearningUnitModuleRepository : IRepositoryBase<LearningUnitModule>
    {
        Task<bool> DeleteByLearningUnitId(int semesterId);
    }
}
