using System;
using System.Collections.Generic;
using System.Text;

namespace Gym_Management_System.Business.DTOs
{
    public class MemberResponseDto
    {
        public int MemberId { get; set; }

        public string FirstName { get; set; }
        public string LastName { get; set; }

        public string Phone { get; set; }

        public string Gender { get; set; }

        public DateTime JoinDate { get; set; }

        public string? Notes { get; set; }
    }
}
