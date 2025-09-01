using todoAppBE.Core.UserAggregate;

namespace todoappBE.Core.Interfaces;

public interface IJwtTokenService
{
  JwtTokenResult GenerateToken(User user);
}
