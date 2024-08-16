using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace MongoDb_Api.Models
{
    public class UserModel {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public ObjectId _id { get; set; }
        [BsonElement("id")]
        public int Id { get; set; }
        [BsonElement("name")]
        public string Name { get; set; } = "";
        [BsonElement("age")]
        public int Age { get; set; }
    }
}
