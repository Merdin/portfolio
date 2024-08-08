using Microsoft.EntityFrameworkCore;
using Library.Models;

namespace Backend.Data
{
    public class DataContext : DbContext
    {
        public DbSet<Module> Modules { get; set; }
        public DbSet<Constraint> Constraints { get; set; }
        public DbSet<User> Users { get; set; }

        public DbSet<StudyPlan> StudyPlans { get; set; }
        public DbSet<LearningUnit> LearningUnits { get; set; }

        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            /* Many-to-many for Module to Constraint */
            modelBuilder.Entity<ModuleConstraint>()
                .HasKey(mc => new {mc.ModuleId, mc.ConstraintId});
            modelBuilder.Entity<ModuleConstraint>()
                .HasOne(mc => mc.Module)
                .WithMany(m => m.ModuleConstraints)
                .HasForeignKey(mc => mc.ModuleId);
            modelBuilder.Entity<ModuleConstraint>()
                .HasOne(mc => mc.Constraint)
                .WithMany(c => c.ModuleConstraints)
                .HasForeignKey(bc => bc.ConstraintId);

            /* Many-to-many for LearningUnit to Module */
            modelBuilder.Entity<LearningUnitModule>()
                .HasKey(lu => new {lu.LearningUnitId, lu.ModuleId});
            modelBuilder.Entity<LearningUnitModule>()
                .HasOne(lu => lu.Unit)
                .WithMany(m => m.LearningUnitModules)
                .HasForeignKey(mc => mc.LearningUnitId);
            modelBuilder.Entity<LearningUnitModule>()
                .HasOne(mc => mc.Module)
                .WithMany(c => c.LearningUnitModules)
                .HasForeignKey(bc => bc.ModuleId);
            
            /* One-to-many for User to Studyplan */
            modelBuilder.Entity<User>()
                .HasMany(user => user.StudyPlans)
                .WithOne(studyPlan => studyPlan.User);
        }
    }
}