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


    }
}
