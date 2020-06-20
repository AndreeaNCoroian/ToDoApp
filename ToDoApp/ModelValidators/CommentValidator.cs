using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ToDoApp.Models;

namespace ToDoApp.ModelValidators
{

	//Requirements lab 3 - Validator for Comment entity
    public class CommentValidator : AbstractValidator<Comment>
	{
		public CommentValidator(TaskItemsDbContext context)
		{
			RuleFor(x => x.Id).NotNull();

			RuleFor(x => x.Text)
				.NotEmpty()
				.WithMessage("Text cannot be empty.");

			RuleFor(x => x.Text)
				.Length(1, 100)
				.WithMessage("Text can't overpass 100 characters.");

			RuleFor(x => x.Important)
				.NotEmpty()
				.WithMessage("You must select the importance of this comment");

		}
	}
}
