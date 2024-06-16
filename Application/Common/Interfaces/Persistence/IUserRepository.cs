using Domain.User;

namespace Application.Common.Interfaces.Persistence;

public interface IUserRepository
{
    List<User> GetUsers();

    User? GetUserByUsername(string username);
    User? GetUserByEmailAndPassword(string username, string password);
    User? GetUserByEmail(string email);
    User? GetUserById(string id);

    void Add(User user);
    void Save(User user);
}