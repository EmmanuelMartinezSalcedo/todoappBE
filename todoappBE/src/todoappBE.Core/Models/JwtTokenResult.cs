namespace todoappBE.Core.Interfaces;

public record JwtTokenResult(string Token, DateTime Expiration);
