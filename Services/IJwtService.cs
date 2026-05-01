using TODOApp.Models;

namespace TODOApp.Services
{
    public interface IJwtService
    {
        string GenerateToken(ApplicationUser user);
        DateTime GetTokenExpiration();
    }
}
