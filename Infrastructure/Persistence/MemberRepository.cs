using System.Threading.Tasks;
using Application.Common.Interfaces;
using Application.Common.Interfaces.Persistence;
using Microsoft.EntityFrameworkCore;
using Domain.Entities;
using Infrastructure.Data;

namespace Infrastructure.Repositories
{
    public class MemberRepository : IMemberRepository
    {
        private readonly UserDbContext _dbContext;

        public MemberRepository(UserDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public Member? GetMemberById(string id)
        {
            return _dbContext.Members.SingleOrDefault(t => t.Id.ToString() == id);
        }

        public List<Member> GetMembers()
        {
            return _dbContext.Members.ToList();
        }

        public List<Role> GetRoles() {
            return _dbContext.Role.ToList();
        }

        public List<Member> GetMembersByTeamId(string teamId)
        {
            return _dbContext.Members.Where(m => m.TeamId.ToString() == teamId).ToList();
        }

        public List<Guid> GetMembersNotInTeam(string teamId)
        {
            var userIdsInTeam = _dbContext.Members.Where(m => m.TeamId.ToString() == teamId).Select(m => m.UserId).ToHashSet();

            return _dbContext.Users.Where(m => !userIdsInTeam.Contains(m.Id)).Select(m => m.Id).Distinct().ToList();
        }

        public List<string> GetRolesByMemberId(string memberId)
        {
            return _dbContext.MemberRole.Where(mr => mr.MemberId.ToString() == memberId).Select(mr => mr.RoleId.ToString()).ToList();
        }

        public List<Guid> GetTeamsIdByUserId(string userId)
        {
            return _dbContext.Members.Where(m => m.UserId.ToString() == userId).Select(m => m.TeamId).ToList();
        }

        public Role? GetRoleById(string roleId)
        {
            return _dbContext.Role.SingleOrDefault(r => r.Id.ToString() == roleId);
        }

        public void Add(Member member)
        {
            _dbContext.Members.Add(member);
            _dbContext.SaveChanges();
        }

        public void Save(Member member)
        {
            _dbContext.Members.Update(member);
            _dbContext.SaveChanges();
        }

        public void AddMemberRole(MemberRole memberRole)
        {
            _dbContext.MemberRole.Add(memberRole);
            _dbContext.SaveChanges();
        }

        public void AddRole(Role role)
        {
            _dbContext.Role.Add(role);
            _dbContext.SaveChanges();
        }
    }
}