namespace Domain.Entities
{
    public class RolePermission
    {
        public Guid Id { get; set; }
        public Guid RoleId { get; set; }
        public Guid PermissionId { get; set; }
        
        // Navigation Properties
        public Role? Role { get; set; }
        public Permission? Permission { get; set; }
    }
}