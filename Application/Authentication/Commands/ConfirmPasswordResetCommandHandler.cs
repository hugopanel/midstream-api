using Application.Common.Interfaces.Persistence;
using Application.Services;
using Domain.User;
using MediatR;

namespace Application.Authentication.Commands;

public class ConfirmPasswordResetCommandHandler : IRequestHandler<ConfirmPasswordResetCommand, Unit>
{
    private IUserRepository _userRepository;
    private UserService _userService;

    public ConfirmPasswordResetCommandHandler(IUserRepository userRepository, UserService userService)
    {
        _userRepository = userRepository;
        _userService = userService;
    }

    public async Task<Unit> Handle(ConfirmPasswordResetCommand request, CancellationToken cancellationToken)
    {
        // Get the user
        User? user = _userRepository.GetUserbyId(request.Id);
        if (user is null)
            throw new Exception("User not found."); // TODO: Create custom exception
        
        // Change the password of the user
        _userService.ChangeUserPassword(user, request.NewPassword);
        
        return Unit.Value; // Return Unit 
    }
}