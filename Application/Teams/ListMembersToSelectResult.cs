using Domain.Entities;

namespace Application.Teams;

public record ListMembersToSelectResult(
    List<MemberToSelect> Members
);

public record MemberToSelect(string userId, string username);