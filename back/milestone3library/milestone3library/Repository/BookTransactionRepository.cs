// BookTransactionRepository
using Microsoft.EntityFrameworkCore;
using milestone3library.Data;
using milestone3library.Entity;
using milestone3library.Interface;

namespace milestone3library.Repository
{


    public class BookTransactionRepository : IBookTransactionRepository
    {
        private readonly libraryDbcontext _context;

        public BookTransactionRepository(libraryDbcontext context)
        {
            _context = context;
        }

        public async Task<BookTransaction> AddTransactionAsync(BookTransaction transaction)
        {
            await _context.Set<BookTransaction>().AddAsync(transaction);
            await SaveChangesAsync();
            return transaction;
        }

        public async Task<BookTransaction> GetTransactionByIdAsync(int id)
        {
            return await _context.Set<BookTransaction>()
                .Include(t => t.Book)
                .Include(t => t.Member)
                .FirstOrDefaultAsync(t => t.Id == id);
        }

        public async Task<List<BookTransaction>> GetAllTransactionsAsync()
        {
            return await _context.Set<BookTransaction>()
                .Include(t => t.Book)
                .Include(t => t.Member)
                .ToListAsync();
        }

        public async Task UpdateTransactionAsync(BookTransaction transaction)
        {
            _context.Set<BookTransaction>().Update(transaction);
            await SaveChangesAsync();
        }

        public async Task<Book> GetBookByIdAsync(int bookId)
        {
            return await _context.Set<Book>().FirstOrDefaultAsync(b => b.Id == bookId);
        }

        public async Task<Member> GetMemberByIdAsync(int memberId)
        {
            return await _context.Set<Member>().FirstOrDefaultAsync(m => m.Id == memberId);
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}
