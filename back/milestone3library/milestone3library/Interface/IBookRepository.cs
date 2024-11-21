using milestone3library.Entity;

namespace milestone3library.Interface
{
    public interface IBookRepository
    {
        Task<IEnumerable<Book>> GetAllBooksAsync();
        Task<Book> GetBookByIdAsync(int id);
        Task AddBookAsync(Book book);
        Task UpdateBookAsync(Book book);
        Task DeleteBookAsync(int id);
        Task RentBookAsync(int id);
        Task ReturnBookAsync(int id);
        //Task<IEnumerable<Book>> GetBooksAsync(int? genreId, int? authorId, int? publicationId);
        Task SaveChangesAsync();
    }
}
