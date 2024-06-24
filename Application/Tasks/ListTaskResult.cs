using Domain.Entities;

namespace Application.Tasks;

public record ListTasksResult(
    List<Tache> Tasks
);