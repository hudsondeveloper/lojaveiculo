
using LojaVeiculos.IRepository;
using LojaVeiculos.Models;
using Microsoft.EntityFrameworkCore;

namespace LojaVeiculos.Data
{
    public class Context : DbContext
    {
        public Context(DbContextOptions<Context> options)
            : base(options)
        {
        }
        public DbSet<Marca> Marca { get; set; }
        public DbSet<Proprietario> Proprietario { get; set; }
        public DbSet<Veiculo> Veiculo { get; set; }


        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<Marca>()
                .HasIndex(u => u.Nome)
                .IsUnique();

            builder.Entity<Veiculo>()
                 .HasIndex(u => u.Renavam)
                 .IsUnique();

            builder.Entity<Proprietario>()
                .HasIndex(u => u.Documento)
                .IsUnique();
        }

    }
}