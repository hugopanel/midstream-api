namespace Api.Models
{
    public record UpdatePasswordRequest(string CurrentPassword, string NewPassword);
}