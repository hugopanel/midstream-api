using System;
using System.Collections.Generic;
using Application.Common.Interfaces;
using Application.Common.Interfaces.Persistence;
using Domain.Entities;
using MongoDB.Driver;

namespace Infrastructure.MongoDb
{
    public class TaskRepository : ITaskRepository
    {
        private readonly MongoDbContext _dbContext;
        private readonly IMongoCollection<Tache> _taskCollection;

        public TaskRepository(MongoDbContext dbContext)
        {
            _dbContext = dbContext;
            _taskCollection = _dbContext.GetCollection<Tache>("Tasks");
        }

        // Récupérer toutes les tâches
        public List<Tache> GetTaches()
        {
            return _taskCollection.Find(FilterDefinition<Tache>.Empty).ToList();
        }

        // Récupérer les tâches par projet
        public List<Tache> GetTachesByProject(string projectId)
        {
            return _taskCollection.Find(t => t.Belong == projectId).ToList();
        }

        // Récupérer les tâches par assigné à
        public List<Tache> GetTachesByAssignee(string projectId, string userId)
        {
            return _taskCollection.Find(t => t.AssignedTo == userId && t.Belong == projectId).ToList();
        }

        // Récupérer les tâches par type
        public List<Tache> GetTachesByType(string projectId, string type)
        {
            return _taskCollection.Find(t => t.TypeOfTask == type && t.Belong == projectId).ToList();
        }

        // Récupérer les tâches par date de fin
        public List<Tache> GetTachesByEndDate(string projectId, DateTime endDate)
        {
            return _taskCollection.Find(t => t.EndDate == endDate && t.Belong == projectId).ToList();
        }

        // Récupérer les tâches par priorité
        public List<Tache> GetTachesByPriority(string projectId, string priority)
        {
            return _taskCollection.Find(t => t.Priority == priority && t.Belong == projectId).ToList();
        }

        // Récupérer une tâche par Id
        public Tache? GetTacheById(string id)
        {
            return _taskCollection.Find(t => t.Id == id).SingleOrDefault();
        }

        // Ajouter une nouvelle tâche
        public void Add(Tache task)
        {
            _taskCollection.InsertOne(task);
        }

        // Enregistrer une tâche
        public void Save(Tache task)
        {
            _taskCollection.ReplaceOne(t => t.Id == task.Id, task);
        }

        // Supprimer une tâche par Id
        public void Delete(string id)
        {
            _taskCollection.DeleteOne(t => t.Id == id);
        }
    }
}
