using Domain.Entities;

namespace Application.Common.Interfaces.Persistence;

public interface IMemberRepository
{
    List<Member> GetMembers();
    List<Member> GetMembersByTeamId(string teamId);

    Member? GetMemberById(string id);

    void Add(Member member);
    void Save(Member member);

    void AddMemberRole(MemberRole memberRole);
    void AddRole(Role role);
}