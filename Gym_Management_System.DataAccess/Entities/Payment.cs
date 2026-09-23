using System;
using System.Collections.Generic;

namespace Gym_Management_System.Entities;

public partial class Payment
{
    public int PaymentId { get; set; }

    public int MembershipId { get; set; }

    public int CreatedByUserId { get; set; }

    public decimal Amount { get; set; }

    public DateTime PaymentDate { get; set; }

    public string? Notes { get; set; }

    public virtual User CreatedByUser { get; set; } = null!;

    public virtual Membership Membership { get; set; } = null!;
}
