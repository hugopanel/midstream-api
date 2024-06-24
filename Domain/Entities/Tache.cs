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
        public string RelatedTo { get; set; }
    }
}


/*
namespace Domain.Entities
{
    public class Tache
    {
        public Guid Id { get; set; }
        public DateTime beginning_date { get; set; }
        public DateTime end_date { get; set; }
        public string priority { get; set; }
        public string status { get; set; }
        public string type_of_task { get; set; }
        public string Title { get; set; } 
        public string description { get; set; }
        public Guid ProjectId { get; set; }
        public string author { get; set; }
        public List<Guid> assigned_to { get; set; }
        public List<Guid> related_to { get; set; }
    }
}*/