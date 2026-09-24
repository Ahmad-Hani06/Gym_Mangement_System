using System;
using System.Collections.Generic;
using System.Text;

namespace Gym_Management_System.Business.DTOs
{
    public class CoachResponseDto
    {
        public int CoachId { get; set; }

        public int PersonId { get; set; }

        public string FirstName { get; set; } = string.Empty;

        public string LastName { get; set; } = string.Empty;

        public string Phone { get; set; } = string.Empty;

        public DateTime HireDate { get; set; }

        public string IsActive { get; set; }

        public string? Notes { get; set; }
    }
}
