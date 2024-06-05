using Application.Common.Interfaces.Persistence;
using Application.Common.Interfaces.Users;
using Domain.User;
using Domain.User.ValueObjects;

namespace Application.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;

        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public void ChangeUserPassword(User user, string newPassword)
        {
            user.ChangePassword(new Password(newPassword));
            _userRepository.Save(user);
        }
    }
}