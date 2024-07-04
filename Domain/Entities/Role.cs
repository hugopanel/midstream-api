using System.ComponentModel.DataAnnotations.Schema;
using Domain.Interfaces;
using Domain.Permissions;

namespace Domain.Entities
{
    public class Role
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        // public List<Permission> Permissions { get; set; } = new List<Permission>();

        private List<string> _permissionCodes = new List<string>();
    
        [NotMapped]
        public List<Permission> Permissions
        {
            get => _permissionCodes.Select(code => PermissionMapper.Permissions.FirstOrDefault(p => p.Code == code)).ToList()!;
            set => _permissionCodes = value.Select(p => p.Code).ToList();
        }

        // EF Core will map this to a column
        public List<string> PermissionCodes
        {
            get => _permissionCodes;
            set => _permissionCodes = value;
        }
    }
}