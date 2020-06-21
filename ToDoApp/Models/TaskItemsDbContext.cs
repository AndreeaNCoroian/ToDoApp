using Microsoft.EntityFrameworkCore;

namespace ToDoApp.Models
{
    public class TaskItemsDbContext : DbContext  //the context is the DB
    {
        public TaskItemsDbContext(DbContextOptions<TaskItemsDbContext> options)
            : base(options)
        {
        }

        //DBSet is the table
        public DbSet<TaskItem> TaskItems { get; set; }
        public DbSet<Comment> Comments { get; set; }
        
    }
}
