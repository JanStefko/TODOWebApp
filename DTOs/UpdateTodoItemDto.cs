using System.ComponentModel.DataAnnotations;

namespace TODOApp.DTOs
{
    public class UpdateTodoItemDto
    {
        [Required]
        [StringLength(200)]
        public string Title { get; set; } = string.Empty;
        public bool IsCompleted { get; set; }
    }
}
