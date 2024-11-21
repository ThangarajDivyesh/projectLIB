using milestone3library.Dto;

namespace milestone3library.Interface
{
    public interface IAuthorService
    {
        Task<IEnumerable<AuthorResponseDto>> GetAllAuthorsAsync();
        Task<AuthorResponseDto> GetAuthorByIdAsync(int id);
        Task AddAuthorAsync(AuthorRequestDto authorRequest);
        Task UpdateAuthorAsync(int id, AuthorRequestDto authorRequest);
        Task DeleteAuthorAsync(int id);


    }
}
