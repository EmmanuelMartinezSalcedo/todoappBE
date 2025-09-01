namespace todoappBE.UseCases.Users.Create;

public record CreateUserCommand(string Name, string Email, string PasswordHash) : ICommand<Result<int>>;
