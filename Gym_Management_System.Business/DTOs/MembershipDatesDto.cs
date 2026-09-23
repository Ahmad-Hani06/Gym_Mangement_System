using System;
using System.Collections.Generic;
using System.Text;

namespace Gym_Management_System.Business.DTOs
{
    public class MembershipDatesDto
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
    }
}
