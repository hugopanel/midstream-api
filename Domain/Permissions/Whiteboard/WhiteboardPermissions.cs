using Domain.Interfaces;

namespace Domain.Permissions.Whiteboard;

public class WhiteboardPermissions : Permission
{
    // Codes
    public const string CreateCode = "Whiteboard.Create";
    public const string SeeCode = "Whiteboard.Read";
    public const string DrawCode = "Whiteboard.Update";
    public const string DeleteCode = "Whiteboard.Delete";
    
    // List of permissions
    public static readonly WhiteboardPermissions Create = new(CreateCode, "Create", "Create a new whiteboard.");
    public static readonly WhiteboardPermissions See = new(SeeCode, "See", "See a whiteboard.");
    public static readonly WhiteboardPermissions Draw = new(DrawCode, "Draw", "Draw and erase on a whiteboard.");
    public static readonly WhiteboardPermissions Delete = new(DeleteCode, "Delete", "Delete a whiteboard.");
    
    // Permission Properties
    public override string Code { get; set; }
    public override string Action { get; set; }
    public override string Description { get; set; }

    private WhiteboardPermissions(string code, string action, string description)
    {
        Code = code;
        Action = action;
        Description = description;
    }
}