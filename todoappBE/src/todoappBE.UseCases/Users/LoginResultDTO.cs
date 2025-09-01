namespace todoappBE.UseCases.Users.Login;

public record LoginResultDTO(string Token, DateTime Expiration, UserDTO User);
