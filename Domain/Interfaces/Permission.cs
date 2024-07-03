using System.Security.Claims;

namespace Domain.Interfaces;

public abstract class Permission
{
    public abstract string Action { get; set; }
    public abstract string Description { get; set; }
    public override string ToString() => Action;
    public string Code => Action.GetHashCode().ToString();

    // Operators 
    // TODO: Replace HashCode comparison with a shorter but unique code
    public static bool operator ==(string claimType, Permission permission)
    {
        return permission.Action.GetHashCode().ToString() == claimType;
    }

    public static bool operator ==(Claim claim, Permission permission)
    {
        return permission.Action.GetHashCode().ToString() == claim.Type;
    }

    public static bool operator !=(string claimType, Permission permission)
    {
        return !(claimType == permission);
    }

    public static bool operator !=(Claim claim, Permission permission)
    {
        return !(claim == permission);
    }
}