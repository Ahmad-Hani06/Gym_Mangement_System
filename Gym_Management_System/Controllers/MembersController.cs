using Microsoft.AspNetCore.Mvc;
using Gym_Management_System.Business;
using Gym_Management_System.Business.Services;
using Microsoft.EntityFrameworkCore.Metadata;

namespace Gym_Management_System.Controllers
{
    [Route("api/[Controller]")]
    [ApiController]
    public class MembersController : ControllerBase
    {

        private readonly MemberService _memberService;
        public MembersController(MemberService memberService)
        {
            _memberService = memberService;
        }

        [HttpPost("AddMember")]

        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        public async Task<IActionResult> RegisterMember(CreateMemberDto memberDto)
        {
            var result = await _memberService.RegisterMemberAsync(memberDto);

            if (result.MemberId == -1)
            {
                if (result.Message == "Person not found.")
                    return NotFound(result.Message);

                if (result.Message == "This person is already registered as a member.")
                    return Conflict(result.Message);
            }

            return StatusCode(StatusCodes.Status201Created, new
            {
                MemberId = result.MemberId,
                Message = result.Message
            });

        }

        [HttpGet("GetAllMembers")]

        [ProducesResponseType(StatusCodes.Status200OK)]

        public async Task<IActionResult> GetAllMembers()
        {
            var Members = await _memberService.GetllMemebrsAsync();

            return Ok(Members);
        }


        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]


        [HttpGet("GetMemberById/{id}", Name = "GetMemberByID")]
        public async Task<IActionResult> GetMemebrByID(int id)
        {
            var member = await _memberService.GetMemberByIDAcync(id);
            if (member == null)
                return NotFound($"Member {id} is not found");

            return Ok(member);
        }

    }
}
