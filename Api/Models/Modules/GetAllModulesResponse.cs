using Domain.Entities;
namespace Api.Models.Modules;

public record GetAllModulesResponse(List<Module> Modules);
