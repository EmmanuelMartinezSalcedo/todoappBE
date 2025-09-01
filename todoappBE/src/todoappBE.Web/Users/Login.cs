using todoappBE.UseCases.Users.Login;

namespace todoappBE.Web.Users;

public class Login(IMediator _mediator) : Endpoint<LoginUserRequest, LoginUserResponse>
{
  public override void Configure()
  {
    Post(LoginUserRequest.Route);
    AllowAnonymous();
    Summary(s =>
    {
      s.ExampleRequest = new LoginUserRequest
      {
        Email = "example@place.com",
        Password = "12345678"
      };

      s.Response(
        StatusCodes.Status200OK,
        "Login successful",
        example: new LoginUserResponse { Message = "Login successful" }
      );

      s.Response(
        StatusCodes.Status401Unauthorized,
        "Invalid email or password",
        example: new LoginUserResponse { Error = "Invalid email or password" }
      );

    });
  }

  public override async Task HandleAsync(LoginUserRequest request, CancellationToken cancellationToken)
  {
    var result = await _mediator.Send(new LoginUserQuery(request.Email!, request.Password!), cancellationToken);

    if (result.IsSuccess)
    {
      var token = result.Value.Token;

      HttpContext.Response.Cookies.Append("AuthToken", token, new CookieOptions
      {
        HttpOnly = true,
        Secure = false,
        SameSite = SameSiteMode.Strict, // CSRF
        Expires = DateTime.UtcNow.AddHours(1)
      });

      Response = new LoginUserResponse
      {
        Message = "Login successful",
        Error = null
      };

      return;
    }

    Response = new LoginUserResponse
    {
      Message = null,
      Error = "Invalid email or password"
    };
    HttpContext.Response.StatusCode = StatusCodes.Status401Unauthorized;
  }
}
