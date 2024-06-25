using MediatR;

namespace Application.Tasks.Commands;

public record CreateTaskCommand(DateTime BeginningDate, DateTime EndDate, string Priority, string Status, string TypeOfTask, string Title, string Description, string Belong, string Author, string AssignedTo, List<string> RelatedTo) : IRequest<TaskResult>;