using Domain.Entities;

namespace Application.Teams;

public record ListUsersToDisplayResult(
    List<UserToDisplay> Users
);

public record UserToDisplay(string userId, string username, string email, string avatar, string colour, List<string> roles);