using Microsoft.EntityFrameworkCore;
using milestone3library.Data;
using milestone3library.Entity;
using milestone3library.Interface;

namespace milestone3library.Repository
{
    public class GenreRepository:IGenreRepository
    {
       private readonly libraryDbcontext _context;

        public GenreRepository(libraryDbcontext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Genre>> GetAllGenresAsync()
        {
            return await _context.Genre.ToListAsync();
        }

        public async Task<Genre> GetGenreByIdAsync(int id)
        {
            return await _context.Genre.FindAsync(id);
        }

        public async Task AddGenreAsync(Genre genre)
        {
            _context.Genre.Add(genre);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateGenreAsync(Genre genre)
        {
            _context.Genre.Update(genre);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteGenreAsync(int id)
        {
            var genre = await GetGenreByIdAsync(id);
            if (genre != null)
            {
                _context.Genre.Remove(genre);
                await _context.SaveChangesAsync();
            }
        }

    }
}
