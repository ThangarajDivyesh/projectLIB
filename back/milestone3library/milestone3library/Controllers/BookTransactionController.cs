using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using milestone3library.Dto;
using milestone3library.Interface;
// BookTransactionController
namespace milestone3library.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookTransactionController : ControllerBase
    {
        private readonly IBookTransactionService _bookTransactionService;

        public BookTransactionController(IBookTransactionService bookTransactionService)
        {
            _bookTransactionService = bookTransactionService;
        }



        [HttpPost("rent")]
        public async Task<IActionResult> RentBook([FromBody] RentBookDTO rentBookDTO)
        {
            try
            {
                var transaction = await _bookTransactionService.RentBookAsync(rentBookDTO);
                return Ok(transaction);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("return")]
        public async Task<IActionResult> ReturnBook([FromBody] ReturnBookDTO returnBookDTO)
        {
            try
            {
                var transaction = await _bookTransactionService.ReturnBookAsync(returnBookDTO);
                return Ok(transaction);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("transactions")]
        public async Task<IActionResult> GetAllTransactions()
        {
            var transactions = await _bookTransactionService.GetAllTransactionsAsync();
            return Ok(transactions);
        }

        [HttpGet("transaction/{id}")]
        public async Task<IActionResult> GetTransactionById(int id)
        {
            try
            {
                var transaction = await _bookTransactionService.GetTransactionByIdAsync(id);
                return Ok(transaction);
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }


        [HttpGet("transactions/member/{memberId}")]
        public async Task<IActionResult> GetTransactionsByMember(int memberId)
        {
            try
            {
                var transactions = await _bookTransactionService.GetTransactionsByMemberIdAsync(memberId);
                if (!transactions.Any())
                {
                    return NotFound("No transactions found for this member.");
                }
                return Ok(transactions);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }



    }
}

