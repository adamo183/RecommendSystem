using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookRecommendation.Datalayer.MongoModel
{
    public class UserPassword
    {
        [BsonElement("_id")]
        public ObjectId _id { get; set; }
        [BsonElement("Login")]
        public string Login { get; set; }
        [BsonElement("Password")]
        public string Password { get; set; }
        [BsonElement("UserId")]
        public int UserId { get; set; }
    }
}
