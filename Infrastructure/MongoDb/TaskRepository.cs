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
    }
}
