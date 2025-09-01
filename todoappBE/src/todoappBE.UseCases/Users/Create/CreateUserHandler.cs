using todoAppBE.Core.UserAggregate;

namespace todoappBE.UseCases.Users.Create;

public class CreateUserHandler(IRepository<User> _repository, IPasswordHasher _passwordHasher)
    : ICommandHandler<CreateUserCommand, Result<int>>
{
  public async Task<Result<int>> Handle(CreateUserCommand request, CancellationToken cancellationToken)
  {
    var existingUser = (await _repository.ListAsync(cancellationToken))
        .FirstOrDefault(u => u.Email == request.Email);

    if (existingUser != null)
    {
      return Result<int>.Conflict("Email already in use");
    }

    var hashedPassword = _passwordHasher.Hash(request.PasswordHash);
    var newUser = new User(request.Name, request.Email, hashedPassword);
    var createdItem = await _repository.AddAsync(newUser, cancellationToken);

    return Result<int>.Success(createdItem.Id);
  }
}
