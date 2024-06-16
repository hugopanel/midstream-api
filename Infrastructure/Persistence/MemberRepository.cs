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