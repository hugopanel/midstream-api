using Domain.Entities;
namespace Application.Common.Interfaces.Persistence;

public interface IModuleRepository
{
    List<Module>? GetAllModules();
}
