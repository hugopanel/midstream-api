using Domain.Entities;
namespace Api.Models.Projects;

public record GetAllProjectsResponse(List<Project> Projects);
