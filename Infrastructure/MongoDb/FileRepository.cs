using System;
using System.Collections.Generic;
using Application.Common.Interfaces.Persistence;
using Domain.Entities;
using MongoDB.Driver;

namespace Infrastructure.MongoDb
{
    public class FileRepository : IFileRepository
    {
        private readonly MongoDbContext _dbContext;
        private readonly IMongoCollection<FileApp> _fileCollection;

        public FileRepository(MongoDbContext dbContext)
        {
            _dbContext = dbContext;
            _fileCollection = _dbContext.GetCollection<FileApp>("Files");
        }

        // Récupérer tous les fichiers appartenant à un certain projet
        public List<FileApp> GetFiles(string belong)
        {
            return _fileCollection.Find(f => f.Belong == belong).ToList();
        }

        // Récupérer un fichier par son Id
        public FileApp GetFile(string id)
        {
            return _fileCollection.Find(f => f.Id == id).SingleOrDefault();
        }

        // Ajouter un nouveau fichier
        public void AddFile(FileApp fileDb)
        {
            try
            {
                _fileCollection.InsertOne(fileDb);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        // Supprimer un fichier
        public void DeleteFile(FileApp fileDb)
        {
            _fileCollection.DeleteOne(f => f.Id == fileDb.Id);
        }
    }
}
