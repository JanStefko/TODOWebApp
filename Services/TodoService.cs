using TODOApp.DTOs;
using TODOApp.Models;
using TODOApp.Repositories;

namespace TODOApp.Services
{
    public class TodoService: ITodoService
    {
        private readonly ITodoRepository _todoRepository;

        public TodoService(ITodoRepository todoRepository)
        {
            _todoRepository = todoRepository;
        }

        public async Task<IEnumerable<TodoItemDto>> GetAllAsync()
        {
            var todos = await _todoRepository.GetAllAsync();

            return todos.Select(todo => new TodoItemDto
            {
                Id = todo.Id,
                Title = todo.Title,
                IsCompleted = todo.IsCompleted
            });
        }

        public async Task<TodoItemDto?> GetByIdAsync(int id)
        {
            var todo = await _todoRepository.GetByIdAsync(id);

            if (todo == null)
            {
                return null;
            }

            return new TodoItemDto
            {
                Id = todo.Id,
                Title = todo.Title,
                IsCompleted = todo.IsCompleted
            };
        }

        public async Task<TodoItemDto> CreateAsync(CreateTodoItemDto createTodoDto)
        {
            var todoItem = new TodoItem
            {
                Title = createTodoDto.Title,
                IsCompleted = false
            };

            var createdTodo = await _todoRepository.AddAsync(todoItem);

            return new TodoItemDto
            {
                Id = createdTodo.Id,
                Title = createdTodo.Title,
                IsCompleted = createdTodo.IsCompleted
            };
        }

        public async Task<bool> UpdateAsync(int id, UpdateTodoItemDto updateTodoDto)
        {
            var existingTodo = await _todoRepository.GetByIdAsync(id);

            if (existingTodo == null)
            {
                return false;
            }

            existingTodo.Title = updateTodoDto.Title;
            existingTodo.IsCompleted = updateTodoDto.IsCompleted;

            await _todoRepository.UpdateAsync(existingTodo);

            return true;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var existingTodo = await _todoRepository.GetByIdAsync(id);

            if (existingTodo == null)
            {
                return false;
            }

            await _todoRepository.DeleteAsync(existingTodo);

            return true;
        }
    }
}
