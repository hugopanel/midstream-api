using Microsoft.VisualBasic;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;


namespace Domain.Entities
{
    public class FileApp
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
        [BsonRequired]
        public string name { get; set; }
        public string path { get; set; }
        [BsonRequired]
        public string extension { get; set; }
        [BsonRequired]
        public string description { get; set; }
        public long size { get; set; }
        [BsonRequired]
        public string Belong { get; set; }
        public DateTime Created_date { get; set; }
        public DateTime Modified_date { get; set; }

        public FileApp(string name, string path, string extension, string description, long size, string belong, DateTime created_date, DateTime modified_date)
        {
            this.name = name;
            this.path = path;
            this.extension = extension;
            this.description = description;
            this.size = size;
            Belong = belong;
            Created_date = created_date;
            Modified_date = modified_date;
        }
    }
}
