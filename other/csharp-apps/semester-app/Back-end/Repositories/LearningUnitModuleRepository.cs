using System.Threading.Tasks;
using Backend.Data;
using Library.Models;
using Microsoft.EntityFrameworkCore;

namespace Backend.Repositories
{
    public class LearningUnitModuleRepository : RepositoryBase<LearningUnitModule>, ILearningUnitModuleRepository
    {
        public LearningUnitModuleRepository(DataContext dataContext) : base(dataContext)
        {
        }

        public async Task<bool> DeleteByLearningUnitId(int semesterId)
        {
            var learningUnitModules = await FindByCondition(mc => mc.LearningUnitId.Equals(semesterId)).ToListAsync();
            foreach (var learningUnitModule in learningUnitModules)
            {
                Delete(learningUnitModule);
            }
            return true;
        }
    }
}
