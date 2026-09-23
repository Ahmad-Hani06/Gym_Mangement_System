using System;
using System.Collections.Generic;

namespace Gym_Management_System.Entities;

public partial class Membership
{
    public int MembershipId { get; set; }

    public int MemberId { get; set; }

    public int SubscriptionTypeId { get; set; }

    public int CreatedByUserId { get; set; }

    public DateTime StartDate { get; set; }

    public DateTime EndDate { get; set; }

    public string Status { get; set; } = null!;
    public decimal Price { get; set; }

    public string? Notes { get; set; }

    public virtual User CreatedByUser { get; set; } = null!; // LoggedIn User

    public virtual Member Member { get; set; } = null!;

    public virtual Payment? Payment { get; set; }

    public virtual SubscriptionType SubscriptionType { get; set; } = null!;
}
