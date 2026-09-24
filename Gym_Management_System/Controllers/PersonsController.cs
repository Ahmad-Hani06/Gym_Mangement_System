using Gym_Management_System.Business.DTOs;
using Gym_Management_System.Business.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Gym_Management_System.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PersonsController : ControllerBase
    {
        private readonly PersonService _personService;
        public PersonsController(PersonService personService)
        {
            _personService = personService;
        }

        [HttpGet("SearchByName")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> SearchByName(string firstName, string lastName)
        {
            var person = await _personService.CheckIfPersonExistsByNameAsync(firstName, lastName);

            if (person == null)
                return NotFound("This Person deos not exist");

            return Ok(person);
        }


        [HttpPost("AddPerson")]

        [ProducesResponseType(StatusCodes.Status201Created)]
        public async Task<IActionResult> AddPersonAsync(PersonDto personDto)
        {
            var person = await _personService.AddPersonAsync(personDto);

            return Ok(person);
        }


        [HttpDelete("DeletePerson/{personId}")]

        public async Task<IActionResult> DeletePersonAsync(int personId)
        {
            var result = await _personService.DeletePersonAsync(personId);
           
            if (result.Deleted)
                return NoContent();

            if (result.Message == "Person not found.")
                return NotFound(result.Message);

            if (result.Message == "Person is linked to another record.")
                return Conflict(result.Message);

            return StatusCode(StatusCodes.Status500InternalServerError, result.Message);

        }

    }
}
