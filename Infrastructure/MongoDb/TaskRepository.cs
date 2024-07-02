using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Application.Common.Interfaces;
using Application.Common.Interfaces.Persistence;
using Domain.Entities;
using MongoDB.Driver;

namespace Infrastructure.MongoDb
{
    public class TaskRepository : ITaskRepository
    {
        private readonly MongoDbContext _dbContext;

        public TaskRepository(MongoDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public List<Tache> GetTaches()
        {
            return _dbContext.GetCollection<Tache>("myCollection").Find(FilterDefinition<Tache>.Empty).ToList();
        }

        public List<Tache> GetTachesByProject(string projectId)
        {
            return _dbContext.GetCollection<Tache>("myCollection").Find(t => t.Belong == projectId).ToList();
        }

        public List<Tache> GetTachesByAssignee(string projectId, string userId)
        {
            return _dbContext.GetCollection<Tache>("myCollection").Find(t => t.AssignedTo == userId && t.Belong == projectId).ToList();
        }

        public List<Tache> GetTachesByType(string projectId, string type)
        {
            return _dbContext.GetCollection<Tache>("myCollection").Find(t => t.TypeOfTask == type && t.Belong == projectId).ToList();
        }

        public List<Tache> GetTachesByEndDate(string projectId, DateTime endDate)
        {
            return _dbContext.GetCollection<Tache>("myCollection").Find(t => t.EndDate == endDate && t.Belong == projectId).ToList();
        }

        public List<Tache> GetTachesByPriority(string projectId, string priority)
        {
            return _dbContext.GetCollection<Tache>("myCollection").Find(t => t.Priority == priority && t.Belong == projectId).ToList();
        }

        public Tache? GetTacheById(string id)
        {
            return _dbContext.GetCollection<Tache>("myCollection").Find(t => t.Id == id).SingleOrDefault();
        }

        public void Add(Tache task)
        {
            _dbContext.GetCollection<Tache>("myCollection").InsertOne(task);
        }

        public void Save(Tache task)
        {
            _dbContext.GetCollection<Tache>("myCollection").ReplaceOne(t => t.Id == task.Id, task);
        }

        public void Delete(string id)
        {
            _dbContext.GetCollection<Tache>("myCollection").DeleteOne(t => t.Id == id);
        }
    }
}
