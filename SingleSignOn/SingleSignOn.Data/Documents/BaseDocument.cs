using MongoDB.Bson.Serialization.Attributes;

namespace SingleSignOn.Data.Documents
{
    public abstract class BaseDocument
    {
        [BsonId]
        [BsonRepresentation(MongoDB.Bson.BsonType.ObjectId)]
        public string Id { get; set; }
    }
}
