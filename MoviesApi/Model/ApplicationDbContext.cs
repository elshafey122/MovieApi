using Microsoft.EntityFrameworkCore;
using MoviesApi.Model;

namespace CruidApplication.Models
{
    public class ApplicationDbContext:DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options):base(options)
        {

        }
        public DbSet<Genre> Genres { set; get; }
        public DbSet<Movie> Movies { set; get; }

    }
}
