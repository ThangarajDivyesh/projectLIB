using Microsoft.EntityFrameworkCore;
using milestone3library.Data;
using milestone3library.Dto;
using milestone3library.Entity;
using milestone3library.Interface;

namespace milestone3library.Repository
{
    public class BookRepository: IBookRepository
    {
        private readonly libraryDbcontext _context;

        public BookRepository(libraryDbcontext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Book>> GetAllBooksAsync()

         =>
        await _context.Books.ToListAsync();





        public async Task<Book> GetBookByIdAsync(int id) =>
            await _context.Books.FindAsync( id);

        public async Task AddBookAsync(Book book)
        {
            await _context.Books.AddAsync(book);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateBookAsync(Book book)
        {
            _context.Books.Update(book);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteBookAsync(int id)
        {
            var book = await GetBookByIdAsync(id);
            if (book != null)
            {
                _context.Books.Remove(book);
                await _context.SaveChangesAsync();
            }
        }

        public async Task RentBookAsync(int id)
        {
            var book = await GetBookByIdAsync(id);
            if (book != null && book.AvailableCopies > 0)
            {
                book.AvailableCopies--;
                await UpdateBookAsync(book);
            }
        }

        public async Task ReturnBookAsync(int id)
        {
            var book = await GetBookByIdAsync(id);
            if (book != null && book.AvailableCopies < book.TotalCopies)
            {
                book.AvailableCopies++;
                await UpdateBookAsync(book);
            }
        }
        //public async Task<IEnumerable<Book>> GetBooksAsync(int? genreId, int? authorId, int? publicationId)
        //{
        //    var query = _context.Books.Include(b => b.Genre).AsQueryable();

        //    if (genreId.HasValue)
        //        query = query.Where(b => b.GenreId == genreId);

   
        //    return await query.ToListAsync();
        //}


        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();  // This commits the changes to the database
        }
    }

 



}

