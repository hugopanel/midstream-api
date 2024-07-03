using Domain.Interfaces;

namespace Domain.Permissions.Whiteboard;

public class WhiteboardPermissions : Permission
{
    // List of permissions
    public static readonly WhiteboardPermissions Create = new("Create", "Create a new whiteboard.");
    public static readonly WhiteboardPermissions See = new("See", "See a whiteboard.");
    public static readonly WhiteboardPermissions Draw = new("Draw", "Draw and erase on a whiteboard.");
    public static readonly WhiteboardPermissions Delete = new("Delete", "Delete a whiteboard.");
    
    // Permission Properties
    public override string Action { get; set; }
    public override string Description { get; set; }

    private WhiteboardPermissions(string action, string description)
    {
        Action = action;
        Description = description;
    }
}