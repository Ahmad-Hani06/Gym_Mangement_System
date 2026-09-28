using Gym_Management_System.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Gym_Management_System.DataAccess.Entities
{
    public partial class RefreshToken
    {
        public int RefreshTokenId { get; set; }

        public int UserId { get; set; }

        public string TokenHash { get; set; } = null!;

        public DateTime ExpiresAt { get; set; }

        public bool IsRevoked { get; set; }

        public virtual User User { get; set; } = null!;
    }
}
