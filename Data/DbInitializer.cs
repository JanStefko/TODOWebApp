using TODOApp.Models;

namespace TODOApp.Data
{
    public static class DbInitializer
    {
        public static void Seed(AppDbContext context)
        {
            if (context.TodoItems.Any())
            {
                return;
            }

            var todos = new List<TodoItem>
            {
                new TodoItem { Title = "Naučit se ASP.NET Core", IsCompleted = true },
                new TodoItem { Title = "Postavit Todo aplikaci", IsCompleted = true },
                new TodoItem { Title = "Přidat autentizaci", IsCompleted = false },
                new TodoItem { Title = "Nasadit na Azure", IsCompleted = false },
                new TodoItem { Title = "Najít první junior pozici", IsCompleted = false }
            };

            context.TodoItems.AddRange(todos);
            context.SaveChanges();
        }
    }
}