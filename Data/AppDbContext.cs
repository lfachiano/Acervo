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
<<<<<<< HEAD
                .HasForeignKey(l => l.AutorId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Livro>()
                .HasMany(l => l.Categorias)
                .WithMany(c => c.Livros);
=======
                .HasForeignKey(l => l.AutorId);
>>>>>>> 4ab22e388cff521e3931eba276f896d57250ee06
        }


        public DbSet<Autor> Autores { get; set; }
        public DbSet<Livro> Livros { get; set; }
<<<<<<< HEAD
        public DbSet<Categoria> Categorias { get; set; }
=======
>>>>>>> 4ab22e388cff521e3931eba276f896d57250ee06
    }

}
