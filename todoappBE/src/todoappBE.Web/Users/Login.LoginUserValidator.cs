using FluentValidation;

namespace todoappBE.Web.Users;

public class LoginUserValidator : Validator<LoginUserRequest>
{
  public LoginUserValidator()
  {
    RuleFor(x => x.Email)
        .NotEmpty().WithMessage("Email is required.")
        .EmailAddress().WithMessage("Email must be a valid email address.")
        .MaximumLength(255).WithMessage("Email cannot exceed 255 characters."); // Ajusta si tienes constante

    RuleFor(x => x.Password)
        .NotEmpty().WithMessage("Password is required.")
        .MinimumLength(8).WithMessage("Password must be at least 8 characters long.")
        .MaximumLength(100).WithMessage("Password cannot exceed 100 characters.");
  }
}
