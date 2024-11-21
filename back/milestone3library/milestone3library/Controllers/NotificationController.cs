using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using milestone3library.Data;

namespace milestone3library.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NotificationController : ControllerBase
    {
        private readonly libraryDbcontext _context;

        public NotificationController(libraryDbcontext context)
        {
            _context = context;
        }

        [HttpGet("{memberId}")]
        public IActionResult GetNotifications(int memberId)
        {
            var notifications = _context.Notifications
                .Where(n => n.MemberId == memberId)
                .OrderByDescending(n => n.NotificationDate)
                .ToList();
            return Ok(notifications);
        }
    }

}
