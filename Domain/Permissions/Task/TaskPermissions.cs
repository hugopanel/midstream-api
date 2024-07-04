using Domain.Interfaces;

namespace Domain.Permissions.Task;

public class TaskPermissions : Permission
{
    // Codes
    public const string CreateCode = "Task.Create";
    public const string ReadCode = "Task.Read";
    public const string UpdateCode = "Task.Update";
    public const string DeleteCode = "Task.Delete";
    public const string AssignCode = "Task.Assign";
    public const string CompleteCode = "Task.Complete";

    // List of permissions
    public static readonly TaskPermissions Create = new(CreateCode, "Create Task", "Create a new task.");
    public static readonly TaskPermissions Read = new(ReadCode, "Read Task", "Read a task.");
    public static readonly TaskPermissions Update = new(UpdateCode, "Update Task", "Update a task.");
    public static readonly TaskPermissions Delete = new(DeleteCode, "Delete Task", "Delete a task.");
    public static readonly TaskPermissions Assign = new(AssignCode, "Assign Task", "Assign a task to a user.");
    public static readonly TaskPermissions Complete = new(CompleteCode, "Complete Task", "Mark a task as complete.");

    // Permission properties
    public override string Code { get; set; }
    public override string Action { get; set; }
    public override string Description { get; set; }

    private TaskPermissions(string code, string action, string description)
    {
        Code = code;
        Action = action;
        Description = description;
    }
}