using System;
using System.Collections.Generic;

namespace Gym_Management_System.Entities;

public partial class SubscriptionType
{
    public int SubscriptionId { get; set; }

    public string Name { get; set; } = null!;

    public int DurationMonths { get; set; }

    public decimal Price { get; set; }

    public string? Notes { get; set; }

    public virtual ICollection<Membership> Memberships { get; set; } = new List<Membership>();
}
