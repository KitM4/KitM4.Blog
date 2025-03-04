using KitM4.Blog.Domain.Common;
using KitM4.Blog.Domain.Communication.Requests;

using FluentValidation;

namespace KitM4.Blog.Core.Validators.Auth;

public class LoginRequestValidator : AbstractValidator<AuthRequests.Login>
{
    public LoginRequestValidator()
    {
        RuleFor(request => request.Name)
            .NotEmpty().WithMessage("Name cannot be empty")
            .MinimumLength(EntityDataLength.MinNameLength).WithMessage($"Name must be at least {EntityDataLength.MinNameLength} characters")
            .MaximumLength(EntityDataLength.MaxNameLength).WithMessage($"Name must be less than {EntityDataLength.MaxNameLength} characters")
            .Matches(@"^[A-Za-z]+(?: [A-Za-z]+)*\.?$").WithMessage("Name can contain only English letters, spaces, and optional '.' at the end");

        RuleFor(request => request.Password)
            .NotEmpty().WithMessage("Password cannot be empty")
            .MinimumLength(EntityDataLength.MinPasswordLength).WithMessage($"Password must be at least {EntityDataLength.MinPasswordLength} characters")
            .MaximumLength(EntityDataLength.MaxPasswordLength).WithMessage($"Password must be less than {EntityDataLength.MaxPasswordLength} characters");
    }
}