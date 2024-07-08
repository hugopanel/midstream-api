namespace Api.Models;

public record CreateTaskRequest(string BeginningDate, string EndDate, string Priority, string Status, string TypeOfTask, string Title, string Description, string Belong, string Author, string AssignedTo, List<string> RelatedTo);