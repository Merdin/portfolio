using Microsoft.EntityFrameworkCore;
using MuzikantenApi.Models;

namespace MuzikantenApi.Data
{
    public class DefaultContext : DbContext
    {
        public DbSet<Muzikant> Muzikant { get; set; }
        public DbSet<Instrument> Instrument { get; set; }
        public DefaultContext(DbContextOptions<DefaultContext> options) : base(options) { }
    }
}