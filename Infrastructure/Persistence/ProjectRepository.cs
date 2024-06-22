using System.Threading.Tasks;
using Application.Common.Interfaces;
using Application.Common.Interfaces.Persistence;
using Microsoft.EntityFrameworkCore;
using Domain.Entities;
using Infrastructure.Data;

namespace Infrastructure.Repositories
{
    public class ProjectRepository : IProjectRepository
    {
        private readonly UserDbContext _dbContext;

        public ProjectRepository(UserDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public Project? GetProjectById(string id)
        {
            return _dbContext.Project.SingleOrDefault(t => t.Id.ToString() == id);
        }

        public List<Project> GetAllProjects()
        {
            return _dbContext.Project.ToList();
        }

        public void Add(Project project)
        {
            _dbContext.Project.Add(project);
            _dbContext.SaveChanges();
        }

        public void Save(Project project)
        {
            _dbContext.Project.Update(project);
            _dbContext.SaveChanges();
        }
    }
}
