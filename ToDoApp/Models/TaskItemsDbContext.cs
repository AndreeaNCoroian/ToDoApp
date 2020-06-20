using Microsoft.EntityFrameworkCore;

namespace ToDoApp.Models
{
    public class TaskItemsDbContext : DbContext
    {
        public TaskItemsDbContext(DbContextOptions<TaskItemsDbContext> options)
            : base(options)
        {
        }

        public DbSet<TaskItem> TaskItems { get; set; }
    }
}
