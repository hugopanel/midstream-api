using Domain.Entities;

namespace Application.Tasks;

public record ListTasksToDisplayResult(
    List<Tache> Tasks,
    List<string> Types,
    List<string> Priorities
);