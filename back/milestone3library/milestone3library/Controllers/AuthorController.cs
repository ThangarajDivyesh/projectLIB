using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using milestone3library.Dto;
using milestone3library.Interface;

namespace milestone3library.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthorController : ControllerBase
    {
        private readonly IAuthorService _authorService;

        public AuthorController(IAuthorService authorService)
        {
            _authorService = authorService;
        }


        [HttpGet]
        public async Task<ActionResult> GetAllAuthors()
        {
            var authors = await _authorService.GetAllAuthorsAsync();
            return Ok(authors);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult> GetAuthorById(int id)
        {
            var author = await _authorService.GetAuthorByIdAsync(id);
            if (author == null)
            {
                return NotFound();
            }

            return Ok(author);
        }

        [HttpPost]
        public async Task<ActionResult> AddAuthor( AuthorRequestDto authorRequest)
        {
            await _authorService.AddAuthorAsync(authorRequest);
            return CreatedAtAction(nameof(GetAuthorById), new { id = authorRequest.Name }, authorRequest);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateAuthor(int id, AuthorRequestDto authorRequest)
        {
            await _authorService.UpdateAuthorAsync(id, authorRequest);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteAuthor(int id)
        {
            await _authorService.DeleteAuthorAsync(id);
            return NoContent();
        }
    }
}
