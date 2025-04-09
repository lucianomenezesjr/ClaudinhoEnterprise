using Microsoft.EntityFrameworkCore;
using ClaudinhoEnterpriseApp.Models;

namespace ClaudinhoEnterpriseApp
{
    public class ClaudinhoEnterpriseDbContext : DbContext
    {
        public ClaudinhoEnterpriseDbContext(DbContextOptions<ClaudinhoEnterpriseDbContext> options)
            :base(options)
        {
        }

        public DbSet<Usuario> Usuarios {get;set;}

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Usuario>().ToTable("usuarios");
        }
    }
}