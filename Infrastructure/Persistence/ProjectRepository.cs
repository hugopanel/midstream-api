﻿using System.Threading.Tasks;
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
            return _dbContext.Projects.SingleOrDefault(t => t.Id.ToString() == id);
        }
        
        public Project? GetProjectByName(string name)
        {
            return _dbContext.Projects.SingleOrDefault(t => t.Name == name);
        }

        public List<Project> GetProjects()
        {
            return _dbContext.Projects.ToList();
        }

        public List<Project> GetProjectsWithoutTeam()
        {
            return _dbContext.Projects.Where(p => !_dbContext.Teams.Any(t => t.ProjectId == p.Id)).ToList();
        }

        public void Add(Project project)
        {
            _dbContext.Projects.Add(project);
            _dbContext.SaveChanges();
        }

        public void Save(Project project)
        {
            _dbContext.Projects.Update(project);
            _dbContext.SaveChanges();
        }
    }
}
