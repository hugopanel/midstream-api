using Domain.Entities;

namespace Application.Teams;

public record ListMembersResult(
    List<Member> Members
);