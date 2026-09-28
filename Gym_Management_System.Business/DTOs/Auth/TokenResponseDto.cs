using System;
using System.Collections.Generic;
using System.Text;

namespace Gym_Management_System.Business.DTOs.Auth
{
    public class TokenResponseDto
    {
        public string AccessToken { get; set; } = string.Empty;
        public string RefreshToken { get; set; } = string.Empty;
    }
}
