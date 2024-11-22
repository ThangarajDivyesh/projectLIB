using milestone3library.Dto;

namespace milestone3library.Interface
{
    public interface IBookTransactionService
    {
        Task<BookTransactionDTO> RentBookAsync(RentBookDTO rentBookDTO);
        Task<BookTransactionDTO> ReturnBookAsync(ReturnBookDTO returnBookDTO);
        Task<List<BookTransactionDTO>> GetAllTransactionsAsync();
        Task<BookTransactionDTO> GetTransactionByIdAsync(int id);
        Task<IEnumerable<BookTransactionDTO>> GetTransactionsByMemberIdAsync(int memberId);
    }

}
