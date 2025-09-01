using System.Text.Json.Serialization;

namespace todoappBE.Web.Users;

public class LoginUserResponse
{
  [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
  public string? Message { get; set; }

  [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
  public string? Error { get; set; }
}
