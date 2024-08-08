using System.Threading.Tasks;
using Backend.Data;

namespace Backend.Repositories
{
    public class RepositoryWrapper : IRepositoryWrapper
    {
        private readonly DataContext _context;
        private IModuleRepository _module;
        private IConstraintRepository _constraint;
        private IStudyPlanRepository _studyPlans;
        private ILearningUnitRepository _learningUnits;
        private IModuleConstraintRepository _moduleConstraints;
        private ILearningUnitModuleRepository _learningUnitModules;
        private IUserRepository _users;

        public RepositoryWrapper(DataContext context)
        {
            _context = context;
        }

        public IModuleRepository Modules => _module ??= new ModuleRepository(_context);

        public IConstraintRepository Constraints
        {
            get { return _constraint ??= new ConstraintRepository(_context); }
        }

        public IStudyPlanRepository StudyPlans
        {
            get { return _studyPlans ??= new StudyPlanRepository(_context); }
        }

        public IUserRepository Users
        {
            get { return _users ??= new UserRepository(_context); }
        }
        public ILearningUnitRepository LearningUnits
        {
            get { return _learningUnits ??= new LearningUnitRepository(_context); }

        }

        public IModuleConstraintRepository ModuleConstraints
        {
            get { return _moduleConstraints ??= new ModuleConstraintRepository(_context); }

        }
        public ILearningUnitModuleRepository LearningUnitModules
        {
            get { return _learningUnitModules ??= new LearningUnitModuleRepository(_context); }

        }

        public async Task Save()
        {
            await _context.SaveChangesAsync();
        }
    }
}
