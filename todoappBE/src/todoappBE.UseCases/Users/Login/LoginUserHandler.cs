using System.Linq;
using Ardalis.SharedKernel;
using todoappBE.Core.Interfaces;
using todoAppBE.Core.UserAggregate;

namespace todoappBE.UseCases.Users.Login;

public class LoginUserHandler(
    IRepository<User> _repository,
    IPasswordHasher _passwordHasher,
    IJwtTokenService _jwtTokenService)
    : IQueryHandler<LoginUserQuery, Result<LoginResultDTO>>
{
  public async Task<Result<LoginResultDTO>> Handle(LoginUserQuery request, CancellationToken cancellationToken)
  {
    var user = (await _repository.ListAsync(cancellationToken))
        .FirstOrDefault(u => u.Email == request.Email);

    if (user == null)
      return Result.NotFound("User not found");

    if (!_passwordHasher.Verify(request.Password, user.PasswordHash))
      return Result.Unauthorized();

    var token = _jwtTokenService.GenerateToken(user);

    var userDto = new UserDTO(user.Id, user.Name, user.Email, user.CreatedAt);

    var resultDto = new LoginResultDTO(token.Token, token.Expiration, userDto);

    return Result.Success(resultDto);
  }
}
