using System.Threading.Tasks;
using Application.Common.Interfaces;
using Application.Common.Interfaces.Persistence;
using Microsoft.EntityFrameworkCore;
using Domain.Entities;
using Infrastructure.Data;

namespace Infrastructure.Repositories
{
    public class TeamRepository : ITeamRepository
    {
        private readonly UserDbContext _dbContext;

        public TeamRepository(UserDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public List<Team> GetTeams()
        {
            return _dbContext.Teams.ToList();
        }

        public Team? GetTeamById(string id)
        {
            return _dbContext.Teams.SingleOrDefault(t => t.Id.ToString() == id);
        }

        public Team? GetTeamByProjectId(string projectId)
        {
            return _dbContext.Teams.SingleOrDefault(t => t.ProjectId.ToString() == projectId);
        }

        public string GetTeamNameById(string id)
        {
            return _dbContext.Teams.SingleOrDefault(t => t.Id.ToString() == id)?.Name;
        }

        public string GetProjectIdByTeamId(string id)
        {
            return _dbContext.Teams.SingleOrDefault(t => t.Id.ToString() == id)?.ProjectId.ToString();
        }


        public void Add(Team team)
        {
            _dbContext.Teams.Add(team);
            _dbContext.SaveChanges();
        }

        public void Save(Team team)
        {
            _dbContext.Teams.Update(team);
            _dbContext.SaveChanges();
        }

        public void Delete(Team team)
        {
            _dbContext.Teams.Remove(team);
            _dbContext.SaveChanges();
        }
    }
}