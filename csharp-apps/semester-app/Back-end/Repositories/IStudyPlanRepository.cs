using System.Collections.Generic;
using System.Threading.Tasks;
using Library.Models;

namespace Backend.Repositories
{
    public interface IStudyPlanRepository : IRepositoryBase<StudyPlan>
    {
        Task<StudyPlan> GetById(int id);

        Task<List<StudyPlan>> GetByUserId(int id);
    }
}
