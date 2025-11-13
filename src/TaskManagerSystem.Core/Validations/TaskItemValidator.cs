using FluentValidation;
using TaskManagerSystem.Core.Entities;

namespace TaskManagerSystem.Core.Validations;

public class TaskItemValidator : AbstractValidator<TaskItem>
{
    public TaskItemValidator()
    {
        RuleFor(u => u.Title)
            .NotEmpty().WithMessage("Title cannot be empty.")
            .MinimumLength(3).WithMessage("The title must have at least 3 characters.")
            .MaximumLength(200).WithMessage("The title must have a maximum of 200 characters.");

        // RuleFor(u => u.User)
        //     .NotNull().WithMessage("User cannot be empty.");

        // RuleFor(t => t.Description)
        //     .MaximumLength(500).WithMessage("The description should be a maximum of 500 characters.")
        //     .When(t => !string.IsNullOrWhiteSpace(t.Description));

        // RuleFor(t => t.DueDate)
        //     .GreaterThan(t => t.CreatedAt)
        //     .When(t => t.DueDate.HasValue)
        //     .WithMessage("The expiration date must be later than the creation date.");

    }
}