using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using Moq;
using TODOApp.DTOs;
using TODOApp.Models;
using TODOApp.Services;

namespace TODOApp.Tests
{
    public class AuthServiceTests
    {
        private readonly Mock<UserManager<ApplicationUser>> _userManagerMock;
        private readonly Mock<SignInManager<ApplicationUser>> _signInManagerMock;
        private readonly Mock<IJwtService> _jwtServiceMock;
        private readonly AuthService _sut;

        public AuthServiceTests()
        {
            // UserManager má složitý konstruktor, použijeme helper
            var userStoreMock = new Mock<IUserStore<ApplicationUser>>();
            _userManagerMock = new Mock<UserManager<ApplicationUser>>(
                userStoreMock.Object, null!, null!, null!, null!, null!, null!, null!, null!);

            // SignInManager má taky složitý konstruktor
            var contextAccessorMock = new Mock<Microsoft.AspNetCore.Http.IHttpContextAccessor>();
            var claimsFactoryMock = new Mock<IUserClaimsPrincipalFactory<ApplicationUser>>();
            _signInManagerMock = new Mock<SignInManager<ApplicationUser>>(
                _userManagerMock.Object,
                contextAccessorMock.Object,
                claimsFactoryMock.Object,
                null!, null!, null!, null!);

            _jwtServiceMock = new Mock<IJwtService>();

            _sut = new AuthService(
                _userManagerMock.Object,
                _signInManagerMock.Object,
                _jwtServiceMock.Object);
        }

        [Fact]
        public async Task RegisterAsync_NewUser_ReturnsSuccessWithToken()
        {
            // Arrange
            var dto = new RegisterDto { Email = "new@test.cz", Password = "test123" };

            _userManagerMock
                .Setup(x => x.FindByEmailAsync(dto.Email))
                .ReturnsAsync((ApplicationUser?)null);

            _userManagerMock
                .Setup(x => x.CreateAsync(It.IsAny<ApplicationUser>(), dto.Password))
                .ReturnsAsync(IdentityResult.Success);

            _jwtServiceMock
                .Setup(x => x.GenerateToken(It.IsAny<ApplicationUser>()))
                .Returns("fake-jwt-token");

            _jwtServiceMock
                .Setup(x => x.GetTokenExpiration())
                .Returns(DateTime.UtcNow.AddHours(1));

            // Act
            var result = await _sut.RegisterAsync(dto);

            // Assert
            Assert.True(result.Success);
            Assert.NotNull(result.Response);
            Assert.Equal("fake-jwt-token", result.Response.Token);
            Assert.Equal(dto.Email, result.Response.Email);
            Assert.Empty(result.Errors);
        }

        [Fact]
        public async Task RegisterAsync_ExistingUser_ReturnsErrorAndNoToken()
        {
            // Arrange
            var dto = new RegisterDto { Email = "existing@test.cz", Password = "test123" };
            var existingUser = new ApplicationUser { Email = dto.Email };

            _userManagerMock
                .Setup(x => x.FindByEmailAsync(dto.Email))
                .ReturnsAsync(existingUser);

            // Act
            var result = await _sut.RegisterAsync(dto);

            // Assert
            Assert.False(result.Success);
            Assert.Null(result.Response);
            Assert.NotEmpty(result.Errors);
            Assert.Contains("už existuje", result.Errors[0]);

            // Důležité: ověříme, že CreateAsync nikdy nebyl volán
            _userManagerMock.Verify(
                x => x.CreateAsync(It.IsAny<ApplicationUser>(), It.IsAny<string>()),
                Times.Never);
        }

        [Fact]
        public async Task RegisterAsync_WeakPassword_ReturnsIdentityErrors()
        {
            // Arrange
            var dto = new RegisterDto { Email = "new@test.cz", Password = "weak" };

            _userManagerMock
                .Setup(x => x.FindByEmailAsync(dto.Email))
                .ReturnsAsync((ApplicationUser?)null);

            var identityErrors = new[]
            {
                new IdentityError { Description = "Heslo je příliš krátké." }
            };

            _userManagerMock
                .Setup(x => x.CreateAsync(It.IsAny<ApplicationUser>(), dto.Password))
                .ReturnsAsync(IdentityResult.Failed(identityErrors));

            // Act
            var result = await _sut.RegisterAsync(dto);

            // Assert
            Assert.False(result.Success);
            Assert.Null(result.Response);
            Assert.Contains("Heslo je příliš krátké.", result.Errors);
        }
    }
}