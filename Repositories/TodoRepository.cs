using Microsoft.EntityFrameworkCore;
using TODOApp.Data;
using TODOApp.Models;

namespace TODOApp.Repositories
{
    public class TodoRepository : ITodoRepository
    {
        private readonly AppDbContext _context;

        public TodoRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<TodoItem>> GetAllForUserAsync(string userId)
        {
            return await _context.TodoItems
                .Where(t => t.UserId == userId)
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task<TodoItem?> GetByIdForUserAsync(int id, string userId)
        {
            return await _context.TodoItems
                .FirstOrDefaultAsync(t => t.Id == id && t.UserId == userId);
        }

        public async Task<TodoItem> AddAsync(TodoItem todoItem)
        {
            _context.TodoItems.Add(todoItem);
            await _context.SaveChangesAsync();
            return todoItem;
        }

        public async Task UpdateAsync(TodoItem todoItem)
        {
            _context.TodoItems.Update(todoItem);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(TodoItem todoItem)
        {
            _context.TodoItems.Remove(todoItem);
            await _context.SaveChangesAsync();
        }
    }
}