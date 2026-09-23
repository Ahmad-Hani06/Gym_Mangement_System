using System;
using System.Collections.Generic;

namespace Gym_Management_System.Entities;

public partial class Member
{
    public int MemberId { get; set; }

    public int PersonId { get; set; }

    public DateTime JoinDate { get; set; }

    public string? Notes { get; set; }

    public virtual ICollection<Membership> Memberships { get; set; } = new List<Membership>();

    public virtual Person Person { get; set; } = null!;
}
