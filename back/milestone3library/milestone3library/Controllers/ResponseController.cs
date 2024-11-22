using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using milestone3library.Data;
using milestone3library.Entity;

namespace milestone3library.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ResponseController : ControllerBase
    {
        private readonly libraryDbcontext _context;

        public ResponseController(libraryDbcontext context)
        {
            _context = context;
        }

        [HttpPost]
        public IActionResult SendResponse([FromBody] BookResponse response)
        {
            var request = _context.BookRequests.Find(response.RequestId);
            if (request == null)
            {
                return NotFound("Request not found.");
            }

            request.IsProcessed = true;
            _context.BookResponses.Add(response);

            // Send notification
            var notification = new Notification
            {
                MemberId = request.MemberId,
                NotificationMessage = response.ResponseMessage
            };
            _context.Notifications.Add(notification);

            _context.SaveChanges();
            return Ok("Response sent successfully.");
        }

        [HttpGet]
        public IActionResult GetResponses()
        {
            var responses = _context.BookResponses.ToList();
            return Ok(responses);
        }

        [HttpGet("{memberId}")]
        public IActionResult GetResponsesByMember(int memberId)
        {
            var notifications = _context.Notifications
                .Where(n => n.MemberId == memberId)
                .ToList();

            if (!notifications.Any())
            {
                return NotFound("No notifications found for this member.");
            }

            return Ok(notifications);
        }


    }
}
