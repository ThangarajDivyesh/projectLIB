using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using milestone3library.Data;
using milestone3library.Entity;

namespace milestone3library.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RequestController : ControllerBase
    {
        private readonly libraryDbcontext _context;

        public RequestController(libraryDbcontext context)
        {
            _context = context;
        }

        [HttpPost]
        public IActionResult CreateRequest([FromBody] BookRequest request)
        {
            _context.BookRequests.Add(request);
            _context.SaveChanges();
            return Ok("Request submitted successfully.");
        }

        [HttpGet]
        public IActionResult GetRequests()
        {
            var requests = _context.BookRequests.Where(r => !r.IsProcessed).ToList();
            return Ok(requests);
        }
    }
}
