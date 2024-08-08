using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using PitchPerfectHearingTest.Models;

namespace PitchPerfectHearingTest.DataAccess
{
    public class ApplicationContext : DbContext
    {
        public DbSet<Appointment> Appointments { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<HearingTestResult> HearingTestResults { get; set; }
        public DbSet<TimeSlot> TimeSlots { get; set; }
        public DbSet<User> Users { get; set; }

        public ApplicationContext()
        {

        }

        public ApplicationContext(DbContextOptions<ApplicationContext> options) : base(options)
        {

        }


        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            var configuration = new ConfigurationBuilder().AddJsonFile("appsettings.json", optional: false).Build();
            
            string server = configuration.GetSection("StorageServer").Value;
            string container = configuration.GetSection("StorageContainer").Value;
            string user = configuration.GetSection("StorageUser").Value;
            string password = configuration.GetSection("StoragePassword").Value;

            var connectionString = $"Server={server};Database={container};User Id={user};Password={password};Trusted_Connection=false";

            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer(connectionString);
            }
        }
    }
}