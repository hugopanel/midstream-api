using Domain.Entities;

namespace Application.Common.Interfaces.Persistence;

public interface IMemberRepository
{
    List<Member> GetMembers();
    List<Role> GetRoles();

    List<Member> GetMembersByTeamId(string teamId);
    List<Guid> GetMembersNotInTeam(string teamId);

    Member? GetMemberById(string id);

    List<string> GetRolesByMemberId(string memberId);
    List<Guid> GetTeamsIdByUserId(string userId);
    Role? GetRoleById(string roleId);

    void Add(Member member);
    void Save(Member member);

    void AddMemberRole(MemberRole memberRole);
    void AddRole(Role role);
}