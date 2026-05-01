using TODOApp.Models;

namespace TODOApp.Repositories
{
    public interface ITodoRepository
    {
        Task<IEnumerable<TodoItem>> GetAllForUserAsync(string userId);
        Task<TodoItem?> GetByIdForUserAsync(int id, string userId);
        Task<TodoItem> AddAsync(TodoItem todoItem);
        Task UpdateAsync(TodoItem todoItem);
        Task DeleteAsync(TodoItem todoItem);
    }
}
