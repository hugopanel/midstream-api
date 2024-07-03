using Domain.Interfaces;

namespace Domain.Permissions.Document;

public class DocumentPermissions : Permission
{
    // List of permissions
    public static readonly DocumentPermissions Create = new("Create", "Create a new document.");
    public static readonly DocumentPermissions Read = new("Read", "Read a document.");
    public static readonly DocumentPermissions Update = new("Update", "Update a document.");
    public static readonly DocumentPermissions Delete = new("Delete", "Delete a document.");
    public static readonly DocumentPermissions MakeReference =
        new("MakeReference", "Add a reference to a document or section in a document.");
    
    // Permission Properties
    public override string Action { get; set; }
    public override string Description { get; set; }

    private DocumentPermissions(string action, string description)
    {
        Action = action;
        Description = description;
    }
}