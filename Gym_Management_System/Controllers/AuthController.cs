using Gym_Management_System.Business.DTOs;
using Gym_Management_System.Business.DTOs.Auth;
using Gym_Management_System.Business.Services;
using Gym_Management_System.DataAccess.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace Gym_Management_System.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]

    public class AuthController : ControllerBase
    {

      
        private readonly UserService _userService;
        private readonly TokenService _tokenService;

        public AuthController(UserService userService, TokenService tokenService)
        {
            _userService = userService;
            _tokenService = tokenService;
        }


        [AllowAnonymous]
        [HttpPost("Login")]
        public async Task<IActionResult> LoginRequest([FromBody] LoginRequestDto loginDto)
        {
            var userAuth = await _userService.FindUserByUserNameAndPasswordAsync(loginDto.Username, loginDto.Password);
            if (userAuth == null)
                return Unauthorized(new { message = "Invalid username or password" });

          
            var Tokens = await _tokenService.CreateTokenPairAsync(userAuth);


            return Ok(Tokens);




        }


        [AllowAnonymous]
        [HttpPost("Refresh")]
        public async Task<IActionResult> Refresh([FromBody] RefreshRequestDto refreshToken)
        {
           

            var tokens = await _tokenService.RefreshTokensAsync(refreshToken.RefreshToken);

            if (tokens == null)
            {
                return Unauthorized(new
                {
                    message =
                        "Invalid or expired refresh token"
                });
            }

            return Ok(tokens);
        }

        // POST: api/Auth/Logout
        [AllowAnonymous]
        [HttpPost("Logout")]
        public async Task<IActionResult> Logout(
            [FromBody] LogoutRequestDto refreshToken)
        {
            await _tokenService.RevokeRefreshTokenAsync(refreshToken.RefreshToken);
            return NoContent();
        }

}
}
