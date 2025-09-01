using Microsoft.AspNetCore.Authorization;

namespace todoappBE.Web.Users;

[Authorize]
public class Logout : EndpointWithoutRequest
{
  public override void Configure()
  {
    Post(LogoutUserRequest.Route);
    AllowAnonymous();

    Summary(s =>
    {
      s.Response(
        StatusCodes.Status200OK,
        "Logout successful",
        example: new LoginUserResponse { Message = "Login successful" }
      );

      s.Response(
        StatusCodes.Status401Unauthorized,
        "Unauthorizeed",
        example: new LoginUserResponse { Error = "Unauthorized" }
      );

    });
   
  }

  public override async Task HandleAsync(CancellationToken ct)
  {
    var userIdClaim = User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value;

    if (string.IsNullOrEmpty(userIdClaim))
    {
      await SendAsync(new LogoutUserResponse
      {
        Message = null,
        Error = "Unauthorized"
      }, StatusCodes.Status401Unauthorized, ct);

      return;
    }

    HttpContext.Response.Cookies.Delete("AuthToken");

    await SendAsync(new LogoutUserResponse
    {
      Message = "Logout successful",
      Error = null
    }, cancellation: ct);
  }
}
