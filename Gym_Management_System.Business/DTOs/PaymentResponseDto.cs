using System;
using System.Collections.Generic;
using System.Text;

namespace Gym_Management_System.Business.DTOs
{
    public class PaymentResponseDto
    {

        public int PaymentId { get; set; }
        public int MembershipId { get; set; }

        public string MemberName { get; set; }

        public decimal Amount { get; set; }
        public DateTime PaymentDate { get; set; }
    }
}
