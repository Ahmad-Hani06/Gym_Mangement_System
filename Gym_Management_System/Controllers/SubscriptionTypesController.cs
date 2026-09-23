using Gym_Management_System.Business.DTOs;
using Gym_Management_System.Business.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics.Eventing.Reader;
using System.Reflection.Metadata.Ecma335;

namespace Gym_Management_System.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SubscriptionTypesController : ControllerBase
    {

        private readonly SubscriptionTypeService _subscriptionTypeService;
        public SubscriptionTypesController(SubscriptionTypeService subscriptionTypeService)
        {
            _subscriptionTypeService = subscriptionTypeService;
        }

        [HttpGet("GetAllSubscriptionTypes")]


        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetAllSubscriptionTypes()
        {
            var SubscriptionTypesDto = await _subscriptionTypeService.GetAllSubscriptionTypeAsync();

            if (SubscriptionTypesDto.Count == 0)
                return NotFound();

            return Ok(SubscriptionTypesDto);
        }



        [HttpPut("UpdateSubscriptionType/{id}")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]

        public async Task<IActionResult> UpdateSubscriptionType(int id, SubscriptionTypeDto subscriptionTypeDto)
        {
            bool result = await _subscriptionTypeService.UpdateSubscriptionType(id, subscriptionTypeDto);

            if (!result)
                return NotFound($"SubscriptionType with ID {id} was not found.");

            return NoContent();
        }


    }
}
