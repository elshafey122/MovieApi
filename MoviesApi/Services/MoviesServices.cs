using Microsoft.EntityFrameworkCore;

namespace MoviesApi.Services
{
    public class MoviesServices : IMoviesServices
    {
        private readonly ApplicationDbContext _context;
        public MoviesServices(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<Movie> Add(Movie movie)
        {
            await _context.AddAsync(movie);
            _context.SaveChanges();
            return movie;
        }
        public Movie Delete(Movie movie)
        {
            _context.Movies.Remove(movie);
            _context.SaveChanges();
            return movie;
        }
        public async Task<IEnumerable<Movie>> GetAll(byte genreid = 0)
        {
            return await _context.Movies.Where(x=>x.GenreId==genreid || genreid == 0)
                .Include(x => x.genres).OrderByDescending(o => o.Rate).ToListAsync();
        }
        public async Task<Movie> GetById(int id)
        {
            return await _context.Movies.Include(x => x.genres).SingleOrDefaultAsync(x => x.Id == id);
        }
        public Movie Update(Movie movie)
        {
            _context.Update(movie);
            _context.SaveChanges();
            return movie;
        }
    }
}
