using todoappBE.Core.Interfaces;

namespace todoappBE.Infrastructure.Identity;

public class BCryptPasswordHasher : IPasswordHasher
{
  public string Hash(string password) => BCrypt.Net.BCrypt.HashPassword(password);

  public bool Verify(string plainPassword, string hashedPassword)
      => BCrypt.Net.BCrypt.Verify(plainPassword, hashedPassword);
}
