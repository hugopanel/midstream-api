using Domain.Entities;
using Domain.Interfaces;

namespace Application.Common.Interfaces.Authentication;

public interface IJwtTokenGenerator
{
    string GenerateRegistrationToken(string email);

    string GenerateLoginToken(Guid id, string username, string firstName, string lastName, string email, string avatar,
        string colour);

    string GenerateLoginToken(Guid id, string username, string firstName, string lastName, string email, string avatar,
        string colour, List<Permission> permissions);

    string GenerateLoginToken(Guid id, string username, string firstName, string lastName, string email, string avatar,
        string colour, List<Role> roles);

    string GeneratePasswordResetToken(Guid id);
}