using Domain.Entities;

namespace Application.Tasks;

public record ListTasksToDisplayResult(
    List<TaskToDisplay> Tasks,
    List<string> Types,
    List<string> Priorities
);

public record TaskToDisplay(
    string Id,
    DateTime BeginningDate,
    DateTime EndDate,
    string Priority,
    string Status,
    string TypeOfTask,
    string Title,
    string Description,
    string Belong,
    string Author,
    string AssignedTo,
    List<string> RelatedTo,
    string Avatar,
    string Colour
);