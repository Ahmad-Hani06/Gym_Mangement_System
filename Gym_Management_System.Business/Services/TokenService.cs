using Gym_Management_System.Business.DTOs.Auth;
using Gym_Management_System.DataAccess;
using Gym_Management_System.DataAccess.Entities;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace Gym_Management_System.Business.Services
{
    public class TokenService
    {
        private readonly RefreshTokenData _refreshTokenData;

        public TokenService(RefreshTokenData refreshTokenData)
        {
            _refreshTokenData = refreshTokenData;
        }

        // تستخدم عند Login
        public async Task<TokenResponseDto> CreateTokenPairAsync(AuthenticatedUserDto user)
        {
            string accessToken = GenerateAccessToken(user);

            string refreshToken = await CreateRefreshTokenAsync(user.UserId);

            return new TokenResponseDto
            {
                AccessToken = accessToken,
                RefreshToken = refreshToken
            };
        }

        // تستخدم عند Refresh
        public async Task<TokenResponseDto?> RefreshTokensAsync(string oldRefreshToken)
        {
            string oldTokenHash = HashRefreshToken(oldRefreshToken);

            var storedToken = await _refreshTokenData.GetValidTokenAsync(oldTokenHash);

            if (storedToken == null)
            {
                return null;
            }

            // إلغاء Refresh Token القديم
            await _refreshTokenData.RevokeAsync(oldTokenHash);

            var user = new AuthenticatedUserDto
            {
                UserId = storedToken.UserId,
                Username = storedToken.User.UserName,
                Role = storedToken.User.Role
            };

            // هنا يتم إنشاء Access Token جديد
            string newAccessToken = GenerateAccessToken(user);

            // وهنا يتم إنشاء Refresh Token جديد
            string newRefreshToken =await CreateRefreshTokenAsync(user.UserId);

            return new TokenResponseDto
            {
                AccessToken = newAccessToken,
                RefreshToken = newRefreshToken
            };
        }

        // تستخدم عند Logout
        public async Task<bool> RevokeRefreshTokenAsync(
            string refreshToken)
        {
            string tokenHash =
                HashRefreshToken(refreshToken);

            return await _refreshTokenData
                .RevokeAsync(tokenHash);
        }

        private static string GenerateAccessToken(
            AuthenticatedUserDto user)
        {
            var claims = new[]
            {
                new Claim(
                    ClaimTypes.NameIdentifier,
                    user.UserId.ToString()),

                new Claim(
                    ClaimTypes.Name,
                    user.Username),

                new Claim(
                    ClaimTypes.Role,
                    user.Role)
            };

            var key = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(
                    "THIS_IS_A_VERY_SECRET_KEY_123456"));

            var credentials = new SigningCredentials(
                key,
                SecurityAlgorithms.HmacSha256);

            var jwtToken = new JwtSecurityToken(
                issuer: "GymManagementSystemAPI",
                audience: "GymManagementSystemClient",
                claims: claims,
                expires: DateTime.UtcNow.AddMinutes(30),
                signingCredentials: credentials);

            return new JwtSecurityTokenHandler()
                .WriteToken(jwtToken);
        }

        private async Task<string> CreateRefreshTokenAsync(
            int userId)
        {
            string refreshToken = GenerateRefreshToken();

            var refreshTokenEntity = new RefreshToken
            {
                UserId = userId,

                TokenHash = HashRefreshToken(refreshToken),

                ExpiresAt = DateTime.UtcNow.AddDays(7),

                IsRevoked = false
            };

            await _refreshTokenData.AddRefreshTokenAsync(refreshTokenEntity);

            return refreshToken;
        }

        private static string GenerateRefreshToken()
        {
            byte[] randomBytes = new byte[64];

            RandomNumberGenerator.Fill(randomBytes);

            return Convert.ToBase64String(
                randomBytes);
        }

        private static string HashRefreshToken(
            string refreshToken)
        {
            byte[] tokenBytes =
                Encoding.UTF8.GetBytes(refreshToken);

            byte[] hashBytes =
                SHA256.HashData(tokenBytes);

            return Convert.ToHexString(hashBytes);
        }
    }
}