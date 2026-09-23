using System;
using System.Collections.Generic;
using System.Text;

namespace Gym_Management_System.Business.DTOs
{
    public class SubscriptionTypeDto
    {
        public int SubscriptionId { get; set; }
        public string Name { get; set; } = null!;
        public int DurationMonths { get; set; }
        public decimal price { get; set; }

    }
}
