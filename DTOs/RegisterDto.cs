using System.ComponentModel.DataAnnotations;

namespace TODOApp.DTOs
{
    public class RegisterDto
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; } = string.Empty;

        [Required]
        [MinLength(6, ErrorMessage = "Heslo musí mít alespoň 6 znaků.")]
        public string Password { get; set; } = string.Empty;
    }
}
