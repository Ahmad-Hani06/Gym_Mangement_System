using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Gym_Management_System.Business
{
    public class CreateMemberDto
    {
        public int PersonId { get; set; }

        public string? Notes { get; set; }
    }
}
