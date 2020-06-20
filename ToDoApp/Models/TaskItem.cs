using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ToDoApp.Models
{
    public enum Importance { Low, Medium, High }
    public enum State { Open, InProgress, Closed }

   /* public class Comment
    {
        public long Id { get; set; }
        public string Text { get; set; }
        public bool Important { get; set; }
    }*/
    public class TaskItem
    {
        public long Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTimeOffset DateAdded { get; set; }
        public DateTimeOffset Deadline { get; set; }
        public Importance Importance { get; set; }
        public State State { get; set; }
        public DateTimeOffset ClosedAt { get; set; }

      //  public List<Comment> Comments { get; set; }
    }
}
