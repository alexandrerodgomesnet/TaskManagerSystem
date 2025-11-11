using FluentValidation;
using TaskManagerSystem.Core.Entities;

namespace TaskManagerSystem.Core.Validations;

public class UserValidator : AbstractValidator<User>
{
    public UserValidator()
    {
        RuleFor(u => u.Name)
            .NotEmpty().WithMessage("Name cannot be empty.")
            .MinimumLength(3).WithMessage("The name must have at least 3 characters.")
            .MaximumLength(200).WithMessage("The name must have a maximum of 200 characters.");

        RuleFor(u => u.Email)
            .NotEmpty().WithMessage("Email cannot be empty.")
            .EmailAddress().WithMessage("The email address provided is invalid.");
    }
}