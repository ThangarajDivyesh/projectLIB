using milestone3library.Dto;
using milestone3library.Entity;
using milestone3library.Interface;

namespace milestone3library.Service
{
    public class GenreService : IGenreService
    {
        private readonly IGenreRepository _genreRepository;

        public GenreService(IGenreRepository genreRepository)
        {
            _genreRepository = genreRepository;
        }

        // Get all genres and map them to GenreResponseDto
        public async Task<IEnumerable<GenreResponseDto>> GetAllGenresAsync()
        {
            var genres = await _genreRepository.GetAllGenresAsync();
            return genres.Select(g => new GenreResponseDto
            {
                Id = g.Id,
                Name = g.Name
            }).ToList(); // Convert to list to ensure materialization before returning.
        }

        // Get genre by ID and return a response DTO
        public async Task<GenreResponseDto> GetGenreByIdAsync(int id)
        {
            var genre = await _genreRepository.GetGenreByIdAsync(id);

            // Return null or throw an exception (based on preference) if genre is not found
            if (genre == null)
                return null; // Consider throwing an exception here instead of returning null for better error handling

            return new GenreResponseDto
            {
                Id = genre.Id,
                Name = genre.Name
            };
        }

        // Add a new genre
        public async Task AddGenreAsync(GenreRequestDTO genreRequest)
        {
            if (genreRequest == null)
                throw new ArgumentNullException(nameof(genreRequest), "Genre request cannot be null");

            // Mapping and adding the new genre
            var genre = new Genre
            {
                Name = genreRequest.Name
            };

            await _genreRepository.AddGenreAsync(genre);
        }

        // Update an existing genre by ID
        public async Task UpdateGenreAsync(int id, GenreRequestDTO genreRequest)
        {
            if (genreRequest == null)
                throw new ArgumentNullException(nameof(genreRequest), "Genre request cannot be null");

            var genre = await _genreRepository.GetGenreByIdAsync(id);
            if (genre == null)
                return; // Handle case where genre doesn't exist (e.g., throw an exception or return error response)

            genre.Name = genreRequest.Name;
            await _genreRepository.UpdateGenreAsync(genre);
        }

        // Delete genre by ID
        public async Task DeleteGenreAsync(int id)
        {
            var genre = await _genreRepository.GetGenreByIdAsync(id);
            if (genre == null)
                return; // Genre doesn't exist, handle this appropriately, maybe throw an exception

            await _genreRepository.DeleteGenreAsync(id);
        }
    }
}
