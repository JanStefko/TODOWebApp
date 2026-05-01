using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using TODOApp.DTOs;
using TODOApp.Services;

namespace TODOApp.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class TodosController : ControllerBase
    {
        private readonly ITodoService _todoService;

        public TodosController(ITodoService todoService)
        {
            _todoService = todoService;
        }

        private string GetUserId()
        {
            return User.FindFirstValue(ClaimTypes.NameIdentifier)
                ?? throw new InvalidOperationException("Uživatel není autentizovaný.");
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<TodoItemDto>>> GetTodos()
        {
            var todos = await _todoService.GetAllAsync(GetUserId());
            return Ok(todos);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<TodoItemDto>> GetTodo(int id)
        {
            var todo = await _todoService.GetByIdAsync(id, GetUserId());

            if (todo == null)
            {
                return NotFound();
            }

            return Ok(todo);
        }

        [HttpPost]
        public async Task<ActionResult<TodoItemDto>> CreateTodo(CreateTodoItemDto createTodoDto)
        {
            var createdTodo = await _todoService.CreateAsync(createTodoDto, GetUserId());

            return CreatedAtAction(nameof(GetTodo), new { id = createdTodo.Id }, createdTodo);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateTodo(int id, UpdateTodoItemDto updateTodoDto)
        {
            var updated = await _todoService.UpdateAsync(id, updateTodoDto, GetUserId());

            if (!updated)
            {
                return NotFound();
            }

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTodo(int id)
        {
            var deleted = await _todoService.DeleteAsync(id, GetUserId());

            if (!deleted)
            {
                return NotFound();
            }

            return NoContent();
        }
    }
}