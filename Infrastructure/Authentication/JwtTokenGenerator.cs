using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Application.Common.Interfaces.Authentication;
using Domain.Entities;
using Domain.Interfaces;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace Infrastructure.Authentication;

public class JwtTokenGenerator : IJwtTokenGenerator
{
    private JwtSettings _jwtSettings;

    public JwtTokenGenerator(IOptions<JwtSettings> jwtSettings)
    {
        _jwtSettings = jwtSettings.Value;
    }

    private string GenerateGenericStringTokenWithClaims(Claim[] claims)
    {
        var signingCredentials =
            new SigningCredentials(new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtSettings.Secret)),
                SecurityAlgorithms.HmacSha256);

        // Generate the token
        var token = new JwtSecurityToken(
            issuer: _jwtSettings.Issuer,
            audience: _jwtSettings.Audience,
            expires: DateTime.UtcNow.AddHours(1),
            claims: claims,
            signingCredentials: signingCredentials);

        return new JwtSecurityTokenHandler().WriteToken(token);
    }

    public string GenerateRegistrationToken(string email)
    {
        // Create claims for the token
        var claims = new[]
        {
            new Claim("action", "register"),
            new Claim("email", email)
        };

        return GenerateGenericStringTokenWithClaims(claims);
    }

    public string GenerateLoginToken(Guid id, string username, string firstName, string lastName, string email, string avatar, string colour)
    {
        // Create claims for the token
        var claims = new[]
        {
            new Claim("action", "login"),
            new Claim("id", id.ToString()),
            new Claim("username", username),
            new Claim("firstName", firstName),
            new Claim("lastName", lastName),
            new Claim("emailAddress", email),
            new Claim("avatar", avatar),
            new Claim("colour", colour)
        };

        return GenerateGenericStringTokenWithClaims(claims);
    }

    [Obsolete("Pass a list of roles instead of permissions!")]
    public string GenerateLoginToken(Guid id, string username, string firstName, string lastName, string email, string avatar,
        string colour, List<Permission> permissions)
    {
        throw new NotSupportedException();
        
        // Create claims for the token
        var claims = new List<Claim>
        {
            new Claim("action", "login"),
            new Claim("id", id.ToString()),
            new Claim("username", username),
            new Claim("firstName", firstName),
            new Claim("lastName", lastName),
            new Claim("emailAddress", email),
            new Claim("avatar", avatar),
            new Claim("colour", colour)
        };
        
        // Add permissions to the claims
        claims.AddRange(permissions.Select(permission => new Claim(permission.Code, permission.Action)));

        return GenerateGenericStringTokenWithClaims(claims.ToArray());
    }

    public string GenerateLoginToken(Guid id, string username, string firstName, string lastName, string email, string avatar,
        string colour, List<Role> roles)
    {
        var claims = new List<Claim>
        {
            new Claim("action", "login"),
            new Claim("id", id.ToString()),
            new Claim("username", username),
            new Claim("firstName", firstName),
            new Claim("lastName", lastName),
            new Claim("emailAddress", email),
            new Claim("avatar", avatar),
            new Claim("colour", colour)
        };
        
        // Add roles to the claims
        claims.AddRange(roles.Select(role => new Claim(ClaimTypes.Role, role.Id.ToString())));
        
        return GenerateGenericStringTokenWithClaims(claims.ToArray());
    }

    public string GeneratePasswordResetToken(Guid id)
    {
        // Create claims for the token
        var claims = new[]
        {
            new Claim("action", "resetPassword"),
            new Claim("userid", id.ToString())
        };

        return GenerateGenericStringTokenWithClaims(claims);
    }
}