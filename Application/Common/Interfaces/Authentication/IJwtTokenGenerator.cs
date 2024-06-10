namespace Application.Common.Interfaces.Authentication;

public interface IJwtTokenGenerator
{
    string GenerateRegistrationToken(string email);
    string GenerateLoginToken(Guid id, string username, string firstName, string lastName, string email, string avatar, string colour);
    string GeneratePasswordResetToken(Guid id);
}