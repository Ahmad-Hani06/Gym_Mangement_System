using System;
using System.Collections.Generic;
using System.Text;

namespace Gym_Management_System.Business.DTOs
{
    public class MembershipDetailsResponseDto
    {
        public int MembershipId { get; set; }

        public int MemberId { get; set; }

        public string FirstName { get; set; } = null!;

        public string LastName { get; set; } = null!;

        public int SubscriptionTypeID { get; set; }

        public string SubscriptionTypeName { get; set; } = null!;

        public int DurationMonths { get; set; }

        public decimal Price { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }

        public string Status { get; set; } = null!;

        public string? Notes { get; set; }
    }
}
