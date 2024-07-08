using System.Collections.Generic;
using System.Linq;
using Application.Common.Interfaces;
using Application.Common.Interfaces.Persistence;
using Domain.Entities;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories
{
    public class ModuleRepository : IModuleRepository
    {
        private readonly UserDbContext _dbContext;

        public ModuleRepository(UserDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public Module? GetModuleById(int id)
        {
            return _dbContext.Modules.SingleOrDefault(m => m.Id == id);
        }

        public Module? GetModuleByName(string name)
        {
            return _dbContext.Modules.SingleOrDefault(m => m.Name == name);
        }

        public List<Module> GetModules()
        {
            return _dbContext.Modules.ToList();
        }
        public List<Module> GetModulesByNames(string[] names)
        {
            return _dbContext.Modules.Where(m => names.Contains(m.Name.ToUpper())).ToList();
        }
        public void Add(Module module)
        {
            _dbContext.Modules.Add(module);
            _dbContext.SaveChanges();
        }

        public void Save(Module module)
        {
            _dbContext.Modules.Update(module);
            _dbContext.SaveChanges();
        }
    }
}
