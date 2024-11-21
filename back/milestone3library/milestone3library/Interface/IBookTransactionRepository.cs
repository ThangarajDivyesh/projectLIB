using milestone3library.Entity;

namespace milestone3library.Interface
{
    public interface IBookTransactionRepository
    {
        Task<BookTransaction> AddTransactionAsync(BookTransaction transaction);
        Task<BookTransaction> GetTransactionByIdAsync(int id);
        Task<List<BookTransaction>> GetAllTransactionsAsync();
        Task UpdateTransactionAsync(BookTransaction transaction);
        Task<Book> GetBookByIdAsync(int bookId);
        Task<Member> GetMemberByIdAsync(int memberId);
        Task SaveChangesAsync();
    }
}
