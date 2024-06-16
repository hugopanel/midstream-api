namespace Domain.Entities
{
    public class Team
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public Guid ProjectId { get; set; }
        public List<Member> Members { get; set; }
    }
}