using Domain.Interfaces;

namespace Domain.Permissions.Administration;

public class AdministrationPermissions : Permission
{
    // List of permissions
    // USERS
    public static readonly AdministrationPermissions DeleteUser = new("Delete User", "Delete an existing user.");
    
    // ROLES
    public static readonly AdministrationPermissions CreateRole = new("Create Role", "Add a new role.");
    public static readonly AdministrationPermissions UpdateRole = new("Update Role", "Edit a role.");
    public static readonly AdministrationPermissions DeleteRole = new("Delete Role", "Delete a role.");
    
    // PROJECTS AND TEAMS
    public static readonly AdministrationPermissions CreateProject = new("Create Project", "Add a new project.");
    public static readonly AdministrationPermissions UpdateProject = new("Update Project", "Edit a project.");
    public static readonly AdministrationPermissions DeleteProject = new("Delete Project", "Delete a project.");
    public static readonly AdministrationPermissions CreateTeam = new("Create Team", "Add a new team.");
    public static readonly AdministrationPermissions UpdateTeam = new("Update Team", "Edit a team.");
    public static readonly AdministrationPermissions DeleteTeam = new("Delete Team", "Delete a team.");
    
    // Permissions properties
    public override string Action { get; set; }
    public override string Description { get; set; }

    private AdministrationPermissions(string action, string description)
    {
        Action = action;
        Description = description;
    }
}