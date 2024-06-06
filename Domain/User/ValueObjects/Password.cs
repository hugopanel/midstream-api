using Microsoft.AspNetCore.Identity;

namespace Domain.User.ValueObjects;

public class Password
{
    private string _hashedPassword;

    public Password(string hashedPassword)
    {
        _hashedPassword = hashedPassword;
    }

    public static Password FromPlainText(string plainTextPassword)
    {
        var passwordHasher = new PasswordHasher<object>();
        var hashedPassword = passwordHasher.HashPassword(null!, plainTextPassword);
        return new Password(hashedPassword);
    }

    public static Password FromHashed(string hashedPassword)
    {
        return new Password(hashedPassword);
    }
    
    public bool Verify(string plainTextPassword)
    {
        var passwordHasher = new PasswordHasher<object>();
        var result = passwordHasher.VerifyHashedPassword(null!, _hashedPassword, plainTextPassword);
        return result == PasswordVerificationResult.Success;
    }

    public override string ToString()
    {
        return _hashedPassword;
    }
}