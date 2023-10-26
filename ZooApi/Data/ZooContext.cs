using Microsoft.EntityFrameworkCore;
using System.Reflection;
using Zoo.Entities;

namespace ZooApi.Data
{
    public class ZooContext : DbContext
    {
        public DbSet<Animal> Animals { get; set; }
        public DbSet<Address> Addresses { get; set; }
        public DbSet<Caretaker> Caretakers { get; set; }

        public ZooContext(DbContextOptions<ZooContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }

    }
}
