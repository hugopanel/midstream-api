using Domain.Interfaces;

namespace Domain.Permissions.File;

public class FilePermissions : Permission
{
    // List of permissions
    public static readonly FilePermissions Create = new("Create File", "Create a new file.");
    public static readonly FilePermissions Read = new("Read File", "See and download a file.");
    // public static readonly FilePermissions Update = new("Update File", "Update a file.");
    public static readonly FilePermissions Delete = new("Delete File", "Delete a file.");
    
    // Permission properties
    public override string Action { get; set; }
    public override string Description { get; set; }
    
    private FilePermissions(string action, string description)
    {
        Action = action;
        Description = description;
    }
}