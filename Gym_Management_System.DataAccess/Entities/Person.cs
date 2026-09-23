using System;
using System.Collections.Generic;

namespace Gym_Management_System.Entities;

public partial class Person
{
    public int PersonId { get; set; }

    public string FirstName { get; set; } = null!;

    public string LastName { get; set; } = null!;

    public bool Gender { get; set; }

    public string Phone { get; set; }

    public virtual Coach? Coach { get; set; }

    public virtual Member? Member { get; set; }

    public virtual User? User { get; set; }
}
