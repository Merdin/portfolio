using System.Collections.Generic;
using System.Threading.Tasks;
using Library.Models;

namespace Backend.Repositories
{
    public interface ILearningUnitRepository : IRepositoryBase<LearningUnit>
    {
        new Task<List<LearningUnit>> GetAll();
        Task<LearningUnit> GetById(int Id);
    }
}
