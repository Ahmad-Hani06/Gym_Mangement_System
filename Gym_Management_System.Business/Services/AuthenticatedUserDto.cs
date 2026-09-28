using System;
using System.Collections.Generic;
using System.Text;

namespace Gym_Management_System.Business.Services
{
    public class AuthenticatedUserDto
    {
        public int UserId { get; set; }
        public string Username { get; set; } = null!;
        public string Role { get; set; } = null!;
    }
}
