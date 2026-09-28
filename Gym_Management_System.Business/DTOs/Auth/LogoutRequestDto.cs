using System;
using System.Collections.Generic;
using System.Text;

namespace Gym_Management_System.Business.DTOs.Auth
{
    public class LogoutRequestDto
    {
        public string RefreshToken { get; set; }
            = string.Empty;
    }
}
