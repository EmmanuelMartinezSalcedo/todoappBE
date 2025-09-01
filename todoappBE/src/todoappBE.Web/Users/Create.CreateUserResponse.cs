using System.Text.Json.Serialization;

namespace todoappBE.Web.Users;

public class CreateUserResponse
{
  [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
  public string? Name { get; set; }

  [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
  public string? Message { get; set; }

  [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
  public string? Error { get; set; }

  public CreateUserResponse(int id, string name)
  {
    Name = name;
  }
  public CreateUserResponse() { }
}
