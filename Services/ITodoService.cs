using TODOApp.DTOs;

namespace TODOApp.Services
{
    public interface ITodoService
    {
        Task<IEnumerable<TodoItemDto>> GetAllAsync();
        Task<TodoItemDto?> GetByIdAsync(int id);
        Task<TodoItemDto> CreateAsync(CreateTodoItemDto createTodoDto);
        Task<bool> UpdateAsync(int id, UpdateTodoItemDto updateTodoDto);
        Task<bool> DeleteAsync(int id);
    }
}
