using System.ComponentModel.DataAnnotations;

namespace todoappBE.Web.Users;

public class LoginUserRequest
{
  public const string Route = "/Users/login";

  [Required]
  public string? Email { get; set; }
  public string? Password { get; set; }
}
