using Gym_Management_System.Business.DTOs;
using Gym_Management_System.Business.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Gym_Management_System.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MembershipsController : ControllerBase
    {
        private readonly MembershipService _membershipService;
        public MembershipsController(MembershipService membershipService)
        {
            _membershipService = membershipService;
        }

        [HttpPost("Add Membership")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public async Task<IActionResult> AddMemberShip([FromBody] MembershipDto membershipDto,[FromQuery] int UserId)
        {
            var membershipResponse = await _membershipService.AddMembershipAsync(membershipDto, UserId);

            return StatusCode(StatusCodes.Status201Created, membershipResponse);
        }

        [HttpGet("FindMembershipByPhone/{Phone}")]

        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]

        public async Task<IActionResult> FindMembershipByPhone(string Phone)
        {
            var membershipDate = await _membershipService.FindMembershipByPhoneAsync(Phone);
            if (membershipDate == null)
                return NotFound("No Membership exists");

            return Ok(membershipDate);
        }


        [HttpGet("GetActiveMemberships")]

        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> GetActiveMembershipsAsync()
        {
            var memberships = await _membershipService.GetActiveMembershipsAsync();

            return Ok(memberships);
        }

        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> GetMembershipById(int id)
        {
            var membership = await _membershipService
                .GetMembershipByMembershipIDAsync(id);

            return Ok(membership);
        }

        [HttpGet("member/{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> GetMembershipsByMemberID(int id)
        {
            var memberships =
                await _membershipService.GetMembershipByMemberIDAsync(id);
            
            if (memberships.Count == 0)
                return NotFound($"There is no Memberships by this Id: {id}");

            return Ok(memberships);
        }
    }
}
