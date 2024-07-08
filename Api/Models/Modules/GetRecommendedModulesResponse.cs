using Domain.Entities;
namespace Api.Models.Modules;

public record GetRecommendedModulesResponse(List<Module> Modules);
