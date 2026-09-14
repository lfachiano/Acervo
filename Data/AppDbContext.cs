using Acervo.Models;
using Microsoft.EntityFrameworkCore;

namespace Acervo.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<Livro> Livros { get; set; }
    }

}
