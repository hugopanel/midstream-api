namespace Domain.Entities
{
    public class Member
    {
        public Guid Id { get; set; }
        public Guid TeamId { get; set; }
        public Guid UserId { get; set; }
        
        // Navigation Properties
        public Team? Team { get; set; }
        public User.User? User { get; set; }
    }
}
