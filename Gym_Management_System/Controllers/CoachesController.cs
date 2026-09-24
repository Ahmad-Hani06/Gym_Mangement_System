using Gym_Management_System.Business.DTOs;
using Gym_Management_System.Business.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Gym_Management_System.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CoachesController : ControllerBase
    {

        private readonly CoachService _coachService;
        public CoachesController(CoachService coachService)
        {
            _coachService = coachService;
        }

        [HttpPost("AddCoach")]

        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]

        public async Task<IActionResult> AddCoach(CreateCoachDto coachDto)
        {
            var result = await _coachService.AddCoach(coachDto);
            if (result.CoachId == -1)
            {
                if (result.Message == "Person not found")
                    return NotFound(result.Message);

                else
                    return Conflict(result.Message);
            }

            return StatusCode(StatusCodes.Status201Created, new
            {
                CoachId = result.CoachId,
                Message = result.Message
            });
        }

        [HttpGet("GetAllCoaches")]
        [ProducesResponseType(StatusCodes.Status200OK)]

        public async Task<IActionResult> GetAllCoaches()
        {
            var coaches = await _coachService.GetAllCoaches();
       
            return Ok(coaches);
        }
}
}
