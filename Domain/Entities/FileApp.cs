using Microsoft.VisualBasic;

namespace Domain.Entities
{
    public class FileApp
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Type { get; set; }
        public int Size { get; set; }
        public DateTime Created_date { get; set; }
        public DateTime Modified_date { get; set; }
        public string Uploaded_by { get; set; }
        public string Path { get; set; }

    }
}
