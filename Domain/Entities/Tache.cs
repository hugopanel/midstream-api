using System;
using System.Collections.Generic;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Domain.Entities
{
    public class Tache
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }

        [BsonElement("beginning_date")]
        public DateTime BeginningDate { get; set; }

        [BsonElement("end_date")]
        public DateTime EndDate { get; set; }

        [BsonElement("priority")]
        public string Priority { get; set; }

        [BsonElement("status")]
        public string Status { get; set; }

        [BsonElement("type_of_task")]
        public string TypeOfTask { get; set; }

        [BsonElement("Title")]
        public string Title { get; set; }

        [BsonElement("description")]
        public string Description { get; set; }

        [BsonElement("Belong")]
        public string Belong { get; set; }

        [BsonElement("author")]
        public string Author { get; set; }

        [BsonElement("assigned_to")]
        public string AssignedTo { get; set; }

        [BsonElement("related_to")]
        public List<string> RelatedTo { get; set; }
    }
}