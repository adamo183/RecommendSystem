using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookRecommendation.Datalayer.MongoModel
{
    public class RatingDb
    {
        [BsonElement("_id")]
        public ObjectId _id { get; set; }
        [BsonElement("userId")]
        public int UserId { get; set; }
        [BsonElement("bookId")]
        public string BookId { get; set; }
        [BsonElement("rating")]
        public int Rating { get; set; }
    }
}
