using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Driver.Core.Authentication;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookRecommendation.Datalayer.MongoModel
{
    public class UserDb
    {
        [BsonElement("_id")]
        public ObjectId _id { get; set; }
        [BsonElement("UserId")]
        public int UserId { get; set; }
        [BsonElement("Username")]
        public string UserName { get; set; }
        [BsonElement("Age")]
        public int Age { get; set; }
        [BsonElement("First Name")]
        public string FirstName { get; set; }
        [BsonElement("Last Name")]
        public string LastName { get; set; }
        [BsonElement("Gender")]
        public string Gender { get; set; }
        [BsonElement("Location")]
        public string Location { get; set; }
    }
}
