using milestone3library.Dto;

namespace milestone3library.Interface
{
    public interface IGenreService
    {
        // Gets all genres and returns them as a collection of GenreResponseDto
        Task<IEnumerable<GenreResponseDto>> GetAllGenresAsync();

        // Gets a genre by its ID and returns it as a GenreResponseDto
        Task<GenreResponseDto> GetGenreByIdAsync(int id);

        // Adds a new genre based on the provided GenreRequestDTO
        Task AddGenreAsync(GenreRequestDTO genreRequest);

        // Updates an existing genre based on the provided genre ID and GenreRequestDTO
        Task UpdateGenreAsync(int id, GenreRequestDTO genreRequest);

        // Deletes a genre based on the provided genre ID
        Task DeleteGenreAsync(int id);
    }
}
