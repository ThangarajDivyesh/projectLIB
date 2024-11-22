using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using milestone3library.Dto;
using milestone3library.Entity;
using milestone3library.Interface;

namespace milestone3library.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookController : ControllerBase
    {
        private readonly IBookService _bookService;

        public BookController(IBookService bookService)
        {
            _bookService = bookService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllBooks()
        {
            try
            {
                var books = await _bookService.GetAllBooksAsync();
                return Ok(books);
            }
            catch (Exception ex)
            {
                // Log exception if needed
                return StatusCode(StatusCodes.Status500InternalServerError, $"Error retrieving books: {ex.Message}");
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetBookById(int id)
        {
            try
            {
                var book = await _bookService.GetBookByIdAsync(id);
                if (book == null)
                    return NotFound($"Book with ID {id} not found.");

                return Ok(book);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Error retrieving book: {ex.Message}");
            }
        }

        [HttpPost]
        public async Task<IActionResult> AddBook([FromBody] BookRequestDto bookRequest)
        {
            try
            {
                await _bookService.AddBookAsync(bookRequest);
                return CreatedAtAction(nameof(GetBookById), new { id = bookRequest.ISBN }, bookRequest);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Error adding book: {ex.Message}");
            }
        }

        [HttpPut("{ISBN}")]
        public async Task<IActionResult> UpdateBook(int ISBN, [FromBody] BookRequestDto bookRequest)
        {
            try
            {
                var bookExists = await _bookService.GetBookByIdAsync(ISBN);
                if (bookExists == null)
                    return NotFound($"Book with ISBN {ISBN} not found.");

                await _bookService.UpdateBookAsync(ISBN, bookRequest);
                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Error updating book: {ex.Message}");
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBook(int id)
        {
            try
            {
                var bookExists = await _bookService.GetBookByIdAsync(id);
                if (bookExists == null)
                    return NotFound($"Book with ISBN {id} not found.");

                await _bookService.DeleteBookAsync(id);
                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Error deleting book: {ex.Message}");
            }
        }

        //[HttpPost("rent/{ISBN}")]
        //public async Task<IActionResult> RentBook(int ISBN)
        //{
        //    try
        //    {
        //        await _bookService.RentBookAsync(ISBN);
        //        return NoContent();
        //    }
        //    catch (Exception ex)
        //    {
        //        return StatusCode(StatusCodes.Status500InternalServerError, $"Error renting book: {ex.Message}");
        //    }
        //}

        //[HttpPost("return/{ISBN}")]
        //public async Task<IActionResult> ReturnBook(int ISBN, [FromQuery] int memberId)
        //{
        //    try
        //    {
        //        await _bookService.ReturnBookAsync(ISBN, memberId);
        //        return NoContent();
        //    }
        //    catch (Exception ex)
        //    {
        //        return StatusCode(StatusCodes.Status500InternalServerError, $"Error returning book: {ex.Message}");
        //    }
        //}

        //[HttpGet("FilterByID")]
        //public async Task<IActionResult> FilterById([FromQuery] int? authorId)
        //{
        //    try
        //    {
        //        var books = await _bookService.FilterBooksByAuthorAsync(authorId);
        //        return Ok(books);
        //    }
        //    catch (Exception ex)
        //    {
        //        return BadRequest($"Error filtering books: {ex.Message}");
        //    }
        //}
    }
}
