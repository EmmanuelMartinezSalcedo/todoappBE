using todoappBE.UseCases.Users.Create;

namespace todoappBE.Web.Users;

public class Create(IMediator _mediator)
    : Endpoint<CreateUserRequest, CreateUserResponse>
{
  public override void Configure()
  {
    Post(CreateUserRequest.Route);
    AllowAnonymous();
    Summary(s =>
    {
      s.ExampleRequest = new CreateUserRequest
      {
        Name = "Jhon Doe",
        Email = "example@place.com",
        Password = "12345678"
      };

      s.Response(
        StatusCodes.Status200OK,
        "User created",
        example: new CreateUserResponse(1, "John Doe")
      );

      s.Response(
        StatusCodes.Status409Conflict,
        "Email already in use",
        example: new CreateUserResponse { Error = "Email already in use" }
      );
    });
  }

  public override async Task HandleAsync(
      CreateUserRequest request,
      CancellationToken cancellationToken)
  {
    var result = await _mediator.Send(
        new CreateUserCommand(request.Name!, request.Email!, request.Password!),
        cancellationToken);

    if (result.IsSuccess)
    {
      Response = new CreateUserResponse(result.Value, request.Name!)
      {
        Message = "User created successfully",
        Error = null
      };
      return;
    }

    if (result.Errors.FirstOrDefault() == "Email already in use")
    {
      var conflictResponse = new CreateUserResponse { Error = "Email already in use" };
      await SendAsync(conflictResponse, StatusCodes.Status409Conflict, cancellationToken);
      return;
    }

    if (result.Status == ResultStatus.Invalid)
    {
      var errorMessage = result.Errors.FirstOrDefault() ?? "Invalid request";

      Response = new CreateUserResponse()
      {
        Message = null,
        Error = errorMessage
      };
      await SendAsync(Response, StatusCodes.Status400BadRequest, cancellationToken);
      return;
    }

    await SendErrorsAsync();
  }
}
