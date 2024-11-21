using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity.Data;
using Microsoft.AspNetCore.Mvc;
using milestone3library.Dto;
using milestone3library.Interface;

namespace milestone3library.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MemberController : ControllerBase
    {
        private readonly IMemberService _memberService;


        public MemberController(IMemberService memberService)
        {
            this._memberService = memberService;
     
        }
        [HttpGet]
        public async Task<IActionResult> GetAllMembers()
        {
            var members = await _memberService.GetAllMembersAsync();
            return Ok(members);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetMemberById(int id)
        {
            var member = await _memberService.GetMemberByIdAsync(id);
            if (member == null) return NotFound();
            return Ok(member);
        }

        [HttpPost]
        public async Task<IActionResult> AddMember([FromBody] MemberRequestDto memberRequest)
        {
            await _memberService.AddMemberAsync(memberRequest);
            return CreatedAtAction(nameof(GetMemberById), new { id = memberRequest.Id }, memberRequest);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateMember(int id, [FromBody] MemberRequestDto memberRequest)
        {
            await _memberService.UpdateMemberAsync(id, memberRequest);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteMember(int id)
        {
            await _memberService.DeleteMemberAsync(id);
            return NoContent();
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] MemberRequestDto memberRequestDto)
        {
            var member = await _memberService.AuthenticateAsync(memberRequestDto.NicNumber, memberRequestDto.Password);

            if (member == null)
            {
                return Unauthorized("Invalid NIC or password.");
            }

            // If authentication is successful, return member information (or token if desired)
            return Ok(new { message = "Login successful", memberId = member.Id });
        }



       


    }
}
