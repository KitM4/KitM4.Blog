using KitM4.Blog.Domain.Common;
using KitM4.Blog.Domain.Communication.Requests;

using FluentValidation;

namespace KitM4.Blog.Core.Validators;

public class RegisterRequestValidator : AbstractValidator<AuthRequests.Register>
{
    public RegisterRequestValidator()
    {
        RuleFor(request => request.Name)
            .NotEmpty().WithMessage("Name cannot be empty")
            .MinimumLength(EntityDataLength.MinNameLength).WithMessage($"Name must be at least {EntityDataLength.MinNameLength} characters")
            .MaximumLength(EntityDataLength.MaxNameLength).WithMessage($"Name must be less than {EntityDataLength.MaxNameLength} characters")
            .Matches(@"^[a-zA-Z0-9_.-]+$").WithMessage("Name can contain only English letters, numbers, and symbols: '_', '.', '-'");

        RuleFor(request => request.Title)
            .NotEmpty().WithMessage("Title cannot be empty")
            .MaximumLength(EntityDataLength.MaxTitleLength).WithMessage($"Title must be less than {EntityDataLength.MaxTitleLength} characters");

        RuleFor(request => request.Bio)
            .MaximumLength(EntityDataLength.MaxBioLength).WithMessage($"Bio must be less than {EntityDataLength.MaxBioLength} characters");

        RuleFor(request => request.Password)
            .NotEmpty().WithMessage("Password cannot be empty")
            .MinimumLength(EntityDataLength.MinPasswordLength).WithMessage($"Password must be at least {EntityDataLength.MinPasswordLength} characters")
            .MaximumLength(EntityDataLength.MaxPasswordLength).WithMessage($"Password must be less than {EntityDataLength.MaxPasswordLength} characters");
    }
}