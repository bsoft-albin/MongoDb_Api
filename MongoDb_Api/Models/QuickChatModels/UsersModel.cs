using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;
using System.ComponentModel.DataAnnotations;

namespace MongoDb_Api.Models.QuickChatModels
{
    public class UserProfile {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public ObjectId _id { get; set; }

        [BsonElement("username")]
        public string UserName { get; set; } = "";
        [BsonElement("email")]
        public string Email { get; set; } = "";
        [BsonElement("password")]
        public string Password { get; set; } = "";
        [BsonElement("name")]
        public string Name { get; set; } = "";
        [BsonElement("profilePic")]
        public string ProfilePic { get; set; } = "";
        [BsonElement("createdDate")]
        public DateTime CreatedAt { get; set; } = new DateTime();
        [BsonElement("updatedDate")]
        public DateTime UpdatedAt { get; set; } = new DateTime();
        [BsonElement("bio")]
        public string Bio { get; set; } = "";
        [BsonElement("location")]
        public string Location { get; set; } = "";
        [BsonElement("phoneNumber")]
        public string PhoneNumber { get; set; } = "";
        [BsonElement("gender")]
        public int Gender { get; set; }
        [BsonElement("birthDate")]
        public string BirthDate { get; set; } = "";
        [BsonElement("status")]
        public int Status { get; set; }
        [BsonElement("lastSeen")]
        public DateTime LastSeen { get; set; } = new DateTime();
        [BsonElement("deviceToken")]
        public string DeviceToken { get; set; } = "";
        [BsonElement("socialLinks")]
        public string SocialLinks { get; set; } = "";
        [BsonElement("IsOnline")]
        public bool IsOnline { get; set; }
        [BsonElement("Friends")]
        public List<string> Friends { get; set; } = new();
        [BsonElement("BlockList")]
        public List<string> BlockList { get; set; } = new();
        [BsonElement("Role")]
        public short Role { get; set; }
        [BsonElement("LastMessage")]
        public string LastMessage { get; set; } = "";
        [BsonElement("MessageCount")]
        public long MessageCount { get; set; }
        [BsonElement("TypingStatus")]
        public bool TypingStatus { get; set; }
        [BsonElement("LastActiveRoom")]
        public string LastActiveRoom { get; set; } = "";
        [BsonElement("PushNotifications")]
        public string PushNotifications { get; set; } = "";
        [BsonElement("IsTwoFactorEnabled")]
        public bool IsTwoFactorEnabled { get; set; }
        [BsonElement("Preferences")]
        public UserPreferences Preferences { get; set; } = new();
        [BsonElement("SocialConnections")]
        public List<SocialLinks> SocialConnections { get; set; } = new();
    }

    public class UserPreferences {
        public string Theme { get; set; } = "Light";
        public bool Notifications { get; set; }
    }

    public class SocialLinks
    {
        public string Platform { get; set; } = "";
        public string Link { get; set; } = "";
    }

    public class UserObject {
        [Required]
        [MaxLength(50)]
        public string UserName { get; set; } = "";
        [Required]
        [MaxLength(50)]
        public string Password { get; set; } = "";
    }
}
