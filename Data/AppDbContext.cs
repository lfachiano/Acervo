using Acervo.Models;
using Microsoft.EntityFrameworkCore;

namespace Acervo.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        protected override void OnModelCreating(
            ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Livro>()
                .HasOne(l => l.Autor)
                .WithMany(a => a.Livros)
                .HasForeignKey(l => l.AutorId);
        }


        public DbSet<Autor> Autores { get; set; }
        public DbSet<Livro> Livros { get; set; }
    }

}
