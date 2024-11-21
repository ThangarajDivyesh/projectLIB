using milestone3library.Dto;
using milestone3library.Entity;
using milestone3library.Interface;
using milestone3library.Repository;

namespace milestone3library.Service
{
    public class AuthorService : IAuthorService
    {
        private readonly IAuthorRepository _authorRepository;

        // Use the interface here
        public AuthorService(IAuthorRepository authorRepository)
        {
            _authorRepository = authorRepository;
        }

        public async Task<IEnumerable<AuthorResponseDto>> GetAllAuthorsAsync()
        {
            var authors = await _authorRepository.GetAllAuthorsAsync();
            return authors.Select(a => new AuthorResponseDto
            {
                Id = a.Id,
                Name = a.Name
            });
        }

        public async Task<AuthorResponseDto> GetAuthorByIdAsync(int id)
        {
            var author = await _authorRepository.GetAuthorByIdAsync(id);
            if (author == null) return null;

            return new AuthorResponseDto
            {
                Id = author.Id,
                Name = author.Name
            };
        }

        public async Task AddAuthorAsync(AuthorRequestDto authorRequest)
        {
            var author = new Author
            {
                Name = authorRequest.Name
            };

            await _authorRepository.AddAuthorAsync(author);
        }

        public async Task UpdateAuthorAsync(int id, AuthorRequestDto authorRequest)
        {
            var author = await _authorRepository.GetAuthorByIdAsync(id);
            if (author != null)
            {
                author.Name = authorRequest.Name;
                await _authorRepository.UpdateAuthorAsync(author);
            }
        }

        public async Task DeleteAuthorAsync(int id)
        {
            await _authorRepository.DeleteAuthorAsync(id);
        }
    }
}
