using Ardalis.SharedKernel;
using FastEndpoints;
using Microsoft.AspNetCore.Authorization;
using todoappBE.UseCases.Users;
using todoAppBE.Core.UserAggregate;

namespace todoappBE.Web.Users;

[Authorize]
public class Me : EndpointWithoutRequest<UserDTO>
{
  private readonly IRepository<User> _userRepository;

  public Me(IRepository<User> userRepository)
  {
    _userRepository = userRepository;
  }

  public override void Configure()
  {
    Get(MeUserRequest.Route);
  }

  public override async Task HandleAsync(CancellationToken ct)
  {
    var userIdClaim = User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value;
    if (string.IsNullOrEmpty(userIdClaim) || !int.TryParse(userIdClaim, out var userId))
    {
      await SendUnauthorizedAsync(ct);
      return;
    }

    var user = await _userRepository.GetByIdAsync(userId, ct);
    if (user == null)
    {
      await SendNotFoundAsync(ct);
      return;
    }

    var userDto = new UserDTO(user.Id, user.Name, user.Email, user.CreatedAt);

    await SendAsync(userDto, cancellation: ct);
  }

}
