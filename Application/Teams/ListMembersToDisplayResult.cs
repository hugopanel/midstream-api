using Domain.Entities;

namespace Application.Teams;

public record ListMembersToDisplayResult(
    List<MemberToDisplay> Members
);

public record MemberToDisplay(string memberId, string username, string email, string avatar, string colour, List<string> roles);