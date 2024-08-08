using System.Threading.Tasks;

namespace Backend.Repositories
{
    public interface IRepositoryWrapper
    {
        IModuleRepository Modules { get; }
        ILearningUnitRepository LearningUnits { get; }
        IConstraintRepository Constraints { get; }
        IStudyPlanRepository StudyPlans { get; }
        IUserRepository Users { get; }
        IModuleConstraintRepository ModuleConstraints { get; }
        ILearningUnitModuleRepository LearningUnitModules { get; }
        Task Save();
    }
}
