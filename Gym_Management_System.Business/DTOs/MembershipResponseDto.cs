using System;
using System.Collections.Generic;
using System.Text;

namespace Gym_Management_System.Business.DTOs
{
    public class MembershipResponseDto
    {
        public int MembershipId { get; set; }
        public int MemberId { get; set; }
        public int SubscriptionTypeId { get; set; }
        public decimal Price { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string Status { get; set; }
    }
}
