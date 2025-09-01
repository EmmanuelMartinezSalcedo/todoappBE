using System.ComponentModel.DataAnnotations;

namespace todoappBE.Web.Users;

public class CreateUserRequest
{
  public const string Route = "/Users";

  [Required]
  public string? Name { get; set; }
  public string? Email { get; set; }
  public string? Password { get; set; }
}
