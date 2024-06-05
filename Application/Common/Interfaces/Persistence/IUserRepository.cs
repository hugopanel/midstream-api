using Domain.User;

namespace Application.Common.Interfaces.Persistence;

public interface IUserRepository
{
    User? GetUserByUsername(string username);
    User? GetUserByUsernameAndPassword(string username, string password);
    User? GetUserByEmail(string email);
    User? GetUserbyId(string id);

    void Add(User user);
    void Save(User user);
}