namespace Application.Common.Interfaces.Authentication;

public interface IJwtTokenGenerator
{
    string GenerateRegistrationToken(Guid id, string username, string firstName, string lastName, string email);
    string GenerateLoginToken(Guid id, string username, string firstName, string lastName, string email);
    string GeneratePasswordResetToken(Guid id);
}