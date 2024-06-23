using Application.Common.Interfaces;
using Application.Common.Interfaces.Authentication;
using Application.Common.Interfaces.Persistence;
using Domain.Entities;
using Application.Teams;
using MediatR;

namespace Application.Teams.Queries;

public class GetUsersQueryHandler(IMemberRepository memberRepository, IUserRepository userRepository, IJwtTokenGenerator jwtTokenGenerator)
    : IRequestHandler<GetUsersQuery, ListUsersToDisplayResult>
{
    private IUserRepository _userRepository = userRepository;
    private IJwtTokenGenerator _jwtTokenGenerator = jwtTokenGenerator;

    public async Task<ListUsersToDisplayResult> Handle(GetUsersQuery query, CancellationToken cancellationToken)
    {
        var users = _userRepository.GetUsers();
        var usersToDisplay = new List<UserToDisplay>();

        foreach(var user in users)
        {
            var roles = new List<string>();
            var userToDisplay = new UserToDisplay(user.Id.ToString(), user.Username, user.Email, user.Avatar, user.Colour, roles);
            usersToDisplay.Add(userToDisplay);
        }

        // Return users
        return new ListUsersToDisplayResult(usersToDisplay);
    }
}