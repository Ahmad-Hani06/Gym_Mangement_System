using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Gym_Management_System.Business.DTOs
{
    public class MembershipDto
    {
        [Required]
        public int MemberId { get; set; }
        [Required]
        public int SubscriptionTypeId { get; set; }
        public string? Notes { get; set; }


    }
}
