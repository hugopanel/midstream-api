namespace Domain.Entities
{
    public class MemberRole
    {
        public Guid Id { get; set; }
        public Guid MemberId { get; set; }
        public Guid RoleId { get; set; }
    }
}
