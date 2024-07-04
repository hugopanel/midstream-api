using Domain.Interfaces;

namespace Domain.Permissions.Administration;

public class AdministrationPermissions : Permission
{
    // List of permissions
    // USERS
    public const string DeleteUserCode = "Admin.User.Delete";
    public static readonly AdministrationPermissions DeleteUser = new(DeleteUserCode, "Delete User", "Delete an existing user.");
    
    // ROLES
    public const string CreateRoleCode = "Admin.Role.Create";
    public static readonly AdministrationPermissions CreateRole = new(CreateRoleCode, "Create Role", "Add a new role.");
    public const string UpdateRoleCode = "Admin.Role.Update";
    public static readonly AdministrationPermissions UpdateRole = new(UpdateRoleCode, "Update Role", "Edit a role.");
    public const string DeleteRoleCode = "Admin.Role.Delete";
    public static readonly AdministrationPermissions DeleteRole = new(DeleteRoleCode, "Delete Role", "Delete a role.");
    
    // PROJECTS AND TEAMS
    public const string CreateProjectCode = "Admin.Project.Create";
    public static readonly AdministrationPermissions CreateProject = new(CreateProjectCode, "Create Project", "Add a new project.");
    public const string UpdateProjectCode = "Admin.Project.Update";
    public static readonly AdministrationPermissions UpdateProject = new(UpdateProjectCode, "Update Project", "Edit a project.");
    public const string DeleteProjectCode = "Admin.Project.Delete";
    public static readonly AdministrationPermissions DeleteProject = new(DeleteProjectCode, "Delete Project", "Delete a project.");
    public const string CreateTeamCode = "Admin.Team.Create";
    public static readonly AdministrationPermissions CreateTeam = new(CreateTeamCode, "Create Team", "Add a new team.");
    public const string UpdateTeamCode = "Admin.Team.Update";
    public static readonly AdministrationPermissions UpdateTeam = new(UpdateTeamCode, "Update Team", "Edit a team.");
    public const string DeleteTeamCode = "Admin.Team.Delete";
    public static readonly AdministrationPermissions DeleteTeam = new(DeleteTeamCode, "Delete Team", "Delete a team.");
    
    // Permissions properties
    public override string Code { get; set; }
    public override string Action { get; set; }
    public override string Description { get; set; }

    private AdministrationPermissions(string code, string action, string description)
    {
        Code = code;
        Action = action;
        Description = description;
    }
}