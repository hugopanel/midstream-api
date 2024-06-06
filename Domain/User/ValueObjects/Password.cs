namespace Domain.User.ValueObjects;

public class Password
{
    private string _hashedPassword;

    public string HashedPassword { get => _hashedPassword; init => _hashedPassword = value; }

    public Password() { }

    public Password(string plaintextValue)
    {
        _hashedPassword = plaintextValue;
    }

    public string Hash(string plaintextValue, out string salt)
    {
        throw new NotImplementedException();
    }

    public string Hash(string plaintextValue, string salt)
    {
        throw new NotImplementedException();
    }

    public string GenerateSalt()
    {
        throw new NotImplementedException();
    }

    public bool Verify(string password)
    {
        // TODO: Implement hashing if the password isn't hashed when it is passed here.
        return _hashedPassword == password;
    }
}