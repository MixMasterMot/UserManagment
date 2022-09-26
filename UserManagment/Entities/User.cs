using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;
using System.Text.Json.Serialization;

namespace UserManagment.Entities
{
    public class User
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
        public string FullName { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public UserRole? UserRole { get; set; }

        // This can be nulled to enable only updating selected fields
        [JsonIgnore]
        public string? Password { get; set; }
    }

    public enum UserRole
    {
        admin,
        editor,
        viewer
    }
}
