using Domain.Entities;

namespace Application.Tasks;

public record TaskToEditResult(
    string Id,
    string BeginningDate,
    string EndDate, 
    string Priority,
    string Status,
    string TypeOfTask,
    string Title,
    string Description,
    string Belong,
    string Author,
    string AssignedTo,
    List<string> RelatedTo
);