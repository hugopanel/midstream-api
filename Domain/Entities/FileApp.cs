namespace Domain.Entities
{
    public class FileApp
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Type { get; set; }
        public int Size { get; set; }
        public DateOnly CreatedDate { get; set; }
        public DateOnly ModifiedDate { get; set; }
        public string uploadedBy { get; set; }
        public string Path { get; set; }

    }
}
