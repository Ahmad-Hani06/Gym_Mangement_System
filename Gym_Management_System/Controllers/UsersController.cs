using Gym_Management_System.Business;
using Gym_Management_System.Business.DTOs;
using Gym_Management_System.Business.Services;
using Gym_Management_System.DataAccess;
using Gym_Management_System.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.FileProviders;

namespace Gym_Management_System.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {

        private readonly UserService _userService;

        public UsersController(UserService userService)
        {
            _userService = userService;
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        public async Task<IActionResult> AddUser(CreateUserDto userDto)
        {
            var result = await _userService.AddUserAsync(userDto);

            if (result.UserID == -1)
            {
                if (result.Message == "Person not found.")
                    return NotFound(result.Message);

                return Conflict(result.Message);
            }

            return StatusCode(StatusCodes.Status201Created, new
            {
                UserId = result.UserID,
                Message = result.Message
            });
        }


        [HttpPatch("{id}/status")]

        public async Task<IActionResult> UpdateUserStatus(int id, string isActive)
        {
            bool updated = await _userService.UpdateUserStatus(id, isActive);

            if (!updated)
                return NotFound($"User {id} not found.");

            return NoContent();
        }


        [HttpGet("GetAllUsers")]
        [ProducesResponseType(StatusCodes.Status200OK)]

        public async Task<IActionResult> GetAllUsers()
        {
            var users = await _userService.GetAllUsersAsync();

            return Ok(users);
        }

        [HttpGet("GetUserBy/{id}")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        
        public async Task<IActionResult> GetUserByUserId(int id)
        {
            var user = await _userService.GetUserByUserIdAsync(id);
            if (user == null)
                return NotFound($"User {id} is not found");

            return Ok(user);
        }

        [HttpPatch("{id}/reset-password")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> ChangeUserPassword(int id, string password)
        {
            var result = await _userService.ChangeUserPasswordAsync(id, password);
            if (!result)
                return NotFound($"User {id} is not found");

            return NoContent();
        }
    }
}
