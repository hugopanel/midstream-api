namespace Api.Models;

public record ProfileResponse(
    Guid Id,
    string Username,
    string FirstName,
    string LastName,
    string Email
);