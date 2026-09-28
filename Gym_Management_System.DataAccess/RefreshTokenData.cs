using Gym_Management_System.Data;
using Gym_Management_System.DataAccess.Entities;
using Gym_Management_System.Entities;
using Microsoft.EntityFrameworkCore;

namespace Gym_Management_System.DataAccess
{
    public class RefreshTokenData
    {
        private readonly AppDbContext _context;

        public RefreshTokenData(AppDbContext context)
        {
            _context = context;
        }

        public async Task AddRefreshTokenAsync(RefreshToken refreshToken)
        {
            var oldRefreshToken = await _context.RefreshTokens.FirstOrDefaultAsync(token => token.UserId == refreshToken.UserId && !token.IsRevoked);

            if (oldRefreshToken != null)
            {
                oldRefreshToken.IsRevoked = true;
            }

            await _context.RefreshTokens.AddAsync(refreshToken);
            await _context.SaveChangesAsync();
        }

        public async Task<RefreshToken?> GetValidTokenAsync(string refreshTokenHash)
        {
            return await _context.RefreshTokens
         .AsNoTracking()
         .Include(token => token.User)
         .FirstOrDefaultAsync(token =>
             token.TokenHash == refreshTokenHash &&
             !token.IsRevoked &&
             token.ExpiresAt > DateTime.UtcNow &&
             token.User.IsActive);

        }

        public async Task<bool> RevokeAsync(string tokenHash)
        {
            var refreshToken = await _context.RefreshTokens.FirstOrDefaultAsync(token => token.TokenHash == tokenHash);

            if (refreshToken == null)
            {
                return false;
            }

            refreshToken.IsRevoked = true;

            await _context.SaveChangesAsync();
            return true;
        }
    }
}