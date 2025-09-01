namespace todoappBE.UseCases.Users;

public record UserDTO(int Id, string Name, string Email, DateTime CreatedAt);

public record UserNameDTO(string Name);
