namespace Api.Models
{
    public record RegisterRequest(
        string Username,
        string FirstName,
        string LastName,
        string Email
    );
}