using Microsoft.EntityFrameworkCore;

namespace MoviesApi.Services
{
    public class GenresServices : IGenresServices
    {
        public readonly ApplicationDbContext _context;
        public GenresServices(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<Genre> Add(Genre genre)
        {
            await _context.Genres.AddAsync(genre);
            _context.SaveChanges();
            return genre;
        }

        public Genre Delete(Genre genre)
        {
            _context.Remove(genre);
            _context.SaveChanges();
            return genre;
        }

        public async Task<IEnumerable<Genre>> GetAll()
        {
            var movies = await _context.Genres.ToListAsync();
            return movies;
        }

        public async Task<Genre> GetById(byte id)
        {
            return await _context.Genres.SingleOrDefaultAsync(g=>g.Id==id);
        }

        public Task<bool> IsValidGenres(byte id)
        {
           return _context.Genres.AnyAsync(x => x.Id == id);
        }

        public  Genre Update(Genre genre)
        {
            _context.Update(genre);
            _context.SaveChanges();
            return genre;
        }
    }
}
