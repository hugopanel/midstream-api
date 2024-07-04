using Domain.Interfaces;

namespace Domain.Permissions.File;

public class FilePermissions : Permission
{
    // Codes
    public const string CreateCode = "File.Create";
    public const string ReadCode = "File.Read";
    // public const string UpdateCode = "File.Update";
    public const string DeleteCode = "File.Delete";
    
    // List of permissions
    public static readonly FilePermissions Create = new(CreateCode, "Create File", "Create a new file.");
    public static readonly FilePermissions Read = new(ReadCode, "Read File", "See and download a file.");
    // public static readonly FilePermissions Update = new(UpdateCode, "Update File", "Update a file.");
    public static readonly FilePermissions Delete = new(DeleteCode, "Delete File", "Delete a file.");
    
    // Permission properties
    public override string Code { get; set; }
    public override string Action { get; set; }
    public override string Description { get; set; }
    
    private FilePermissions(string code, string action, string description)
    {
        Code = code;
        Action = action;
        Description = description;
    }
}