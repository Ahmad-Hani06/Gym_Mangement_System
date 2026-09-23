using System;
using System.Collections.Generic;
using System.Text;

namespace Gym_Management_System.Business.DTOs
{
    public class ActiveMembershipResponseDto
    {
        public int MembershipId { get; set; }

        public int MemberId { get; set; }

        public int SubscriptionTypeID { get; set; }
        public int DurationMonths { get; set; }

        public decimal Price { get; set; }
        public string FirstName { get; set; }

        public string LastName { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }
    }
}
