using Domain.Entities;

namespace Application.Common.Interfaces.Persistence;

public interface IMemberRepository
{
    List<Member> GetMembers();
    List<Role> GetRoles();

    List<Member> GetMembersByTeamId(string teamId);
    List<Guid> GetMembersNotInTeam(string teamId);

    Member? GetMemberById(string id);
    MemberRole? GetMemberRole(string memberId, string roleId);

    List<string> GetRolesByMemberId(string memberId);
    List<MemberRole> GetMemberRolesByMemberId(string memberId);
    List<Guid> GetTeamsIdByUserId(string userId);
    Role? GetRoleById(string roleId);

    void Add(Member member);
    void Save(Member member);
    void Delete(Member member);

    void AddMemberRole(MemberRole memberRole);
    void DeleteMemberRole(MemberRole memberRole);

    void AddRole(Role role);
}