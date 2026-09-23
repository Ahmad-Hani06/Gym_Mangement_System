using System;
using System.Collections.Generic;

namespace Gym_Management_System.Entities;

public partial class Coach
{
    public int CoachId { get; set; }

    public int PersonId { get; set; }

    public DateTime HireDate { get; set; }

    public string? Notes { get; set; }

    public virtual Person Person { get; set; } = null!;
}
