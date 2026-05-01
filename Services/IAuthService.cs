using TODOApp.DTOs;

namespace TODOApp.Services
{
    public interface IAuthService
    {
        Task<AuthResult> RegisterAsync(RegisterDto registerDto);
        Task<AuthResult> LoginAsync(LoginDto loginDto);
    }

    public class AuthResult
    {
        public bool Success { get; set; }
        public AuthResponseDto? Response { get; set; }
        public List<string> Errors { get; set; } = new();
    }
}