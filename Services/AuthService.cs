using Microsoft.AspNetCore.Identity;
using TODOApp.DTOs;
using TODOApp.Models;

namespace TODOApp.Services
{
    public class AuthService : IAuthService
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly IJwtService _jwtService;

        public AuthService(
            UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager,
            IJwtService jwtService)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _jwtService = jwtService;
        }

        public async Task<AuthResult> RegisterAsync(RegisterDto registerDto)
        {
            // 1. Zkontroluj, jestli uživatel už neexistuje
            var existingUser = await _userManager.FindByEmailAsync(registerDto.Email);
            if (existingUser != null)
            {
                return new AuthResult
                {
                    Success = false,
                    Errors = new List<string> { "Uživatel s tímto e-mailem už existuje." }
                };
            }

            // 2. Vytvoř nového uživatele
            var newUser = new ApplicationUser
            {
                UserName = registerDto.Email,
                Email = registerDto.Email
            };

            var createResult = await _userManager.CreateAsync(newUser, registerDto.Password);

            if (!createResult.Succeeded)
            {
                return new AuthResult
                {
                    Success = false,
                    Errors = createResult.Errors.Select(e => e.Description).ToList()
                };
            }

            // 3. Vygeneruj JWT token a vrať odpověď
            var token = _jwtService.GenerateToken(newUser);

            return new AuthResult
            {
                Success = true,
                Response = new AuthResponseDto
                {
                    Token = token,
                    Email = newUser.Email,
                    ExpiresAt = _jwtService.GetTokenExpiration()
                }
            };
        }

        public async Task<AuthResult> LoginAsync(LoginDto loginDto)
        {
            // 1. Najdi uživatele podle e-mailu
            var user = await _userManager.FindByEmailAsync(loginDto.Email);

            if (user == null)
            {
                return new AuthResult
                {
                    Success = false,
                    Errors = new List<string> { "Nesprávné přihlašovací údaje." }
                };
            }

            // 2. Ověř heslo
            var passwordValid = await _signInManager.CheckPasswordSignInAsync(
                user, loginDto.Password, lockoutOnFailure: false);

            if (!passwordValid.Succeeded)
            {
                return new AuthResult
                {
                    Success = false,
                    Errors = new List<string> { "Nesprávné přihlašovací údaje." }
                };
            }

            // 3. Vygeneruj JWT token a vrať odpověď
            var token = _jwtService.GenerateToken(user);

            return new AuthResult
            {
                Success = true,
                Response = new AuthResponseDto
                {
                    Token = token,
                    Email = user.Email ?? string.Empty,
                    ExpiresAt = _jwtService.GetTokenExpiration()
                }
            };
        }
    }
}