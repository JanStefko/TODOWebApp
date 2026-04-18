using System.ComponentModel.DataAnnotations;

namespace TODOApp.DTOs
{
    public class CreateTodoItemDto
    {
        [Required]
        [StringLength(200)]
        public string Title { get; set; } = string.Empty;
    }
}
