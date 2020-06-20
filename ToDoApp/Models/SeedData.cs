using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Linq;

namespace ToDoApp.Models
{
    public class SeedData
    {
        public static void Initialize(IServiceProvider serviceProvider)
        {
            using (var context = new TaskItemsDbContext(serviceProvider.GetRequiredService<DbContextOptions<TaskItemsDbContext>>()))
            {
                // Look for any taskitems.
                if (context.TaskItems.Any())
                {
                    return;   // DB table has been seeded
                }

                context.TaskItems.AddRange(
                    new TaskItem
                    {

                        Title = "DotNet Lab1",
                        Description = "Lab1 DotNet as per example done in class ",
                        DateAdded = DateTimeOffset.Now,
                        Deadline = DateTimeOffset.Parse("2020-5-16"),
                        Importance = Importance.High,
                        State = State.Closed,
                        ClosedAt = DateTimeOffset.Parse("2020-5-27")
                    },

                    new TaskItem
                    {

                        Title = "DotNet Lab2",
                        Description = "Lab2 DotNet with swagger",
                        DateAdded = DateTimeOffset.Now,
                        Deadline = DateTimeOffset.Parse("2020-6-14"),
                        Importance = Importance.High,
                        State = State.InProgress
                    },

                    new TaskItem
                    {

                        Title = "Java Book",
                        Description = "Read chapters 10, 11 from my Java book",
                        DateAdded = DateTimeOffset.Now,
                        Deadline = DateTimeOffset.Parse("2020-09-01"),
                        Importance = Importance.Medium,
                        State = State.InProgress
                    },

                    new TaskItem
                    {

                        Title = "Learn Spanish",
                        Description = "Improve spanish by doing 2 lessons/day on Duolingo",
                        DateAdded = DateTimeOffset.Now,
                        Deadline = DateTimeOffset.Parse("2020-12-30"),
                        Importance = Importance.Low,
                        State = State.Open
                    }

                );
                context.SaveChanges();
            }
        }
    }
}
