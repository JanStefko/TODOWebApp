using Microsoft.AspNetCore.Identity;
using TODOApp.Models;

namespace TODOApp.Data
{
    public static class DbInitializer
    {
        public static async Task SeedAsync(AppDbContext context, UserManager<ApplicationUser> userManager)
        {
            // Pokud už nějaké úkoly existují, neděláme nic
            if (context.TodoItems.Any())
            {
                return;
            }

            // 1. Najdi nebo vytvoř testovacího uživatele
            var testEmail = "demo@todoapp.cz";
            var testUser = await userManager.FindByEmailAsync(testEmail);

            if (testUser == null)
            {
                testUser = new ApplicationUser
                {
                    UserName = testEmail,
                    Email = testEmail
                };
                await userManager.CreateAsync(testUser, "Demo123!");
            }

            // 2. Vytvoř testovací úkoly pro tohoto uživatele
            var todos = new List<TodoItem>
            {
                new TodoItem { Title = "Naučit se ASP.NET Core", IsCompleted = true, UserId = testUser.Id },
                new TodoItem { Title = "Postavit Todo aplikaci", IsCompleted = true, UserId = testUser.Id },
                new TodoItem { Title = "Přidat autentizaci", IsCompleted = true, UserId = testUser.Id },
                new TodoItem { Title = "Nasadit na Azure", IsCompleted = false, UserId = testUser.Id },
                new TodoItem { Title = "Najít první junior pozici", IsCompleted = false, UserId = testUser.Id }
            };

            context.TodoItems.AddRange(todos);
            await context.SaveChangesAsync();
        }
    }
}