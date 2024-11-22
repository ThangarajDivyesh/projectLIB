using milestone3library.Dto;

namespace milestone3library.Interface
{
    public interface IBookService
    {
        Task<IEnumerable<BookResponseDto>> GetAllBooksAsync();
        Task<BookResponseDto> GetBookByIdAsync(int id);
        //Task RentBookAsync(int id);
        //Task ReturnBookAsync(int id, int memberId);
        Task<BookResponseDto> AddBookAsync(BookRequestDto bookRequest);
        Task UpdateBookAsync(int id, BookRequestDto bookRequest);
        Task DeleteBookAsync(int id);
       
        //Task<IEnumerable<BookResponseDto>> GetBooksAsync(int? genreId);

    }
}
