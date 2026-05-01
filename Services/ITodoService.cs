using TODOApp.DTOs;

namespace TODOApp.Services
{
    public interface ITodoService
    {
        Task<IEnumerable<TodoItemDto>> GetAllAsync(string userId);
        Task<TodoItemDto?> GetByIdAsync(int id, string userId);
        Task<TodoItemDto> CreateAsync(CreateTodoItemDto createTodoDto, string userId);
        Task<bool> UpdateAsync(int id, UpdateTodoItemDto updateTodoDto, string userId);
        Task<bool> DeleteAsync(int id, string userId);
    }
}
