using Domain.Entities;

namespace Application.Teams;

public record ListProjectsResult(
    List<Project> Projects
);