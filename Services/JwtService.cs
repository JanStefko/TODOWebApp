using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using TODOApp.Models;

namespace TODOApp.Services
{
    public class JwtService : IJwtService
    {
        private readonly IConfiguration _configuration;

        public JwtService(IConfiguration configuration)
        {
            _configuration = configuration;
        }


        public string GenerateToken(ApplicationUser user)
        {
            // 1. Načti konfiguraci
            var key = _configuration["Jwt:Key"]
                ?? throw new InvalidOperationException("JWT Key není nakonfigurován.");
            var issuer = _configuration["Jwt:Issuer"];
            var audience = _configuration["Jwt:Audience"];

            // 2. Vytvoř security key z tajného klíče
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            // 3. Vytvoř claims (informace v tokenu)
            var claims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.Id),
                new Claim(JwtRegisteredClaimNames.Email, user.Email ?? string.Empty),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            };

            // 4. Sestav samotný token
            var token = new JwtSecurityToken(
                issuer: issuer,
                audience: audience,
                claims: claims,
                expires: GetTokenExpiration(),
                signingCredentials: credentials
            );

            // 5. Vrať jako string
            return new JwtSecurityTokenHandler().WriteToken(token);

        }

        public DateTime GetTokenExpiration()
        {
            var minutes = int.Parse(_configuration["Jwt:ExpirationInMinutes"] ?? "60");
            return DateTime.UtcNow.AddMinutes(minutes);
        }
    }
}
