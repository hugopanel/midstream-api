using Domain.Interfaces;

namespace Domain.Permissions.Document;

public class DocumentPermissions : Permission
{
    // Codes
    public const string CreateCode = "Doc.Create";
    public const string ReadCode = "Doc.Read";
    public const string UpdateCode = "Doc.Update";
    public const string DeleteCode = "Doc.Delete";
    public const string MakeReferenceCode = "Doc.Reference.Create";

    // List of permissions
    public static readonly DocumentPermissions Create = new(CreateCode, "Create", "Create a new document.");
    public static readonly DocumentPermissions Read = new(ReadCode, "Read", "Read a document.");
    public static readonly DocumentPermissions Update = new(UpdateCode, "Update", "Update a document.");
    public static readonly DocumentPermissions Delete = new(DeleteCode, "Delete", "Delete a document.");
    public static readonly DocumentPermissions MakeReference =
        new(MakeReferenceCode, "MakeReference", "Add a reference to a document or section in a document.");

    // Permission Properties
    public override string Code { get; set; }
    public override string Action { get; set; }
    public override string Description { get; set; }

    private DocumentPermissions(string code, string action, string description)
    {
        Code = code;
        Action = action;
        Description = description;
    }
}