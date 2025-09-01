namespace todoappBE.Web.Users;

public class CreateUserResponse
{
  public int Id { get; set; }
  public string Name { get; set; }
  public string? Message { get; set; }
  public string? Error { get; set; }

  public CreateUserResponse(int id, string name)
  {
    Id = id;
    Name = name;
  }
  public CreateUserResponse() { }
}
