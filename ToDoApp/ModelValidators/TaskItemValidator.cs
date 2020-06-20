using FluentValidation;
using System;
using ToDoApp.Models;

namespace ToDoApp.ModelValidators
{
    public class TaskItemValidator : AbstractValidator<TaskItem>
	{
		public TaskItemValidator(TaskItemsDbContext context)
		{
			RuleFor(x => x.Id).NotNull();

			RuleFor(x => x.Title)
				.NotEmpty()
				.WithMessage("Title cannot be empty");

			RuleFor(x => x.DateAdded)
				.LessThan(DateTime.Now)
				.WithMessage("Date added for a task can't be greater than current date.");

			

			RuleFor(x => x.Description)
				.Length(1, 60)
				.WithMessage("Description can't be overpass 60 characters.");
		}
	}
}
