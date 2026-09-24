using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Gym_Management_System.Business.DTOs
{
    public class CreateCoachDto
    {
        [Required]
        public int PersonId { get; set; }

        public string? Notes { get; set; }
    }
}
