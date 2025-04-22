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
        public DbSet<ContatoLogado> ContatosLogado {get;set;}

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Usuario>().ToTable("usuarios");
            modelBuilder.Entity<ContatoLogado>().ToTable("contatoslogado");

            modelBuilder.Entity<ContatoLogado>()
                .HasOne(c => c.Usuario)
                .WithMany(u => u.Contatos)
                .HasForeignKey(c => c.IdUsuario);
        }
    }
}