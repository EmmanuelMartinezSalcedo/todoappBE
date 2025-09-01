using todoappBE.Infrastructure.Data.Config;
using FluentValidation;

namespace todoappBE.Web.Users;

public class CreateUserValidator : Validator<CreateUserRequest>
{
  public CreateUserValidator()
  {
    RuleFor(x => x.Name)
      .NotEmpty().WithMessage("Name is required.")
      .MinimumLength(2).WithMessage("Name must be at least 2 characters long.")
      .MaximumLength(DataSchemaConstants.DEFAULT_NAME_LENGTH).WithMessage($"Name cannot exceed {DataSchemaConstants.DEFAULT_NAME_LENGTH} characters.");

    RuleFor(x => x.Email)
      .NotEmpty().WithMessage("Email is required.")
      .EmailAddress().WithMessage("Email must be a valid email address.")
      .MaximumLength(DataSchemaConstants.DEFAULT_EMAIL_LENGTH).WithMessage($"Email cannot exceed {DataSchemaConstants.DEFAULT_EMAIL_LENGTH} characters.");

    RuleFor(x => x.Password)
      .NotEmpty().WithMessage("Password is required.")
      .MinimumLength(8).WithMessage("Password must be at least 8 characters long.")
      .MaximumLength(100).WithMessage("Password cannot exceed 100 characters.");
  }
}
