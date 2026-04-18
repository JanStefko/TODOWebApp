using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using TODOApp.Models;

namespace TODOApp.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {
        }

        public DbSet<TodoItem> TodoItems { get; set; } = null!;
    }
}