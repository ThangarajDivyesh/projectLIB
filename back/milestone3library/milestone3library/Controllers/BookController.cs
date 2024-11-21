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
            var books = await _bookService.GetAllBooksAsync();
            return Ok(books);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetBookById(int id)
        {
            var book = await _bookService.GetBookByIdAsync(id);
            if (book == null)
                return NotFound();
            return Ok(book);
        }

        [HttpPost]
        public async Task<IActionResult> AddBook([FromBody] BookRequestDto bookRequest)
        {
          
            await _bookService.AddBookAsync(bookRequest);
            return CreatedAtAction(nameof(GetBookById), new { ISBN = bookRequest.ISBN }, bookRequest);
        }

        [HttpPut("{ISBN}")]
        public async Task<IActionResult> UpdateBook(int id, [FromBody] BookRequestDto bookRequest)
        {
            await _bookService.UpdateBookAsync(id, bookRequest);
            return NoContent();
        }

        [HttpDelete("{ISBN}")]
        public async Task<IActionResult> DeleteBook(int id)
        {
            await _bookService.DeleteBookAsync(id);
            return NoContent();
        }

        //[HttpPost("rent/{ISBN}")]
        //public async Task<IActionResult> RentBook(int id)
        //{
        //    await _bookService.RentBookAsync(id);
        //    return NoContent();
        //}

        //[HttpPost("return/{ISBN}")]
        //public async Task<IActionResult> ReturnBook(int id, int memberId)
        //{
        //    await _bookService.ReturnBookAsync(id ,memberId);
        //    return NoContent();
        //}

        //[HttpGet("FilterByID")]
        //public async Task<IActionResult> FilterById(int? authorId)
        //{
        //    try
        //    {
        //        var books = await _bookService.GetAllBooksAsync();
        //        return Ok(books);

        //    }
        //    catch (Exception ex)
        //    {
        //        return BadRequest("Not Found");
        //    }
        //}

    }
}
