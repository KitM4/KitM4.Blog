using KitM4.Blog.Domain.Communication.Requests;

using FluentValidation;

namespace KitM4.Blog.Core.Validators;

public class ChangeRoleRequestValidator : AbstractValidator<AuthRequests.ChangeRole>
{
    public ChangeRoleRequestValidator()
    {
        RuleFor(request => request.UserId)
            .NotEmpty().WithMessage("User ID cannot be empty")
            .NotEqual(Guid.Empty).WithMessage("User ID must be a valid GUID");

        RuleFor(request => request.NewRole)
            .IsInEnum().WithMessage("NewRole must be a valid user role");
    }
}