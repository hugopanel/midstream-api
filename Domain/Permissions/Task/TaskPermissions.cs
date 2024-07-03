using Domain.Interfaces;

namespace Domain.Permissions.Task;

public class TaskPermissions : Permission
{
    // List of permissions
    public static readonly TaskPermissions Create = new("Create Task", "Create a new task.");
    public static readonly TaskPermissions Read = new("Read Task", "Read a task.");
    public static readonly TaskPermissions Update = new("Update Task", "Update a task.");
    public static readonly TaskPermissions Delete = new("Delete Task", "Delete a task.");
    public static readonly TaskPermissions Assign = new("Assign Task", "Assign a task to a user.");
    public static readonly TaskPermissions Complete = new("Complete Task", "Mark a task as complete.");
    
    // Permission properties
    public override string Action { get; set; }
    public override string Description { get; set; }

    private TaskPermissions(string action, string description)
    {
        Action = action;
        Description = description;
    }
}