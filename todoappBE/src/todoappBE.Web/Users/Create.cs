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
      s.ExampleRequest = new CreateUserRequest { Name = "Jhon Doe", Email = "example@place.com", PasswordHash="12345678" };
    });
  }

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
        PasswordHash = "12345678"
      };
    });
  }

  public override async Task HandleAsync(
      CreateUserRequest request,
      CancellationToken cancellationToken)
  {
    var result = await _mediator.Send(
        new CreateUserCommand(request.Name!, request.Email!, request.PasswordHash!),
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
