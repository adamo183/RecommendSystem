using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson.Serialization.Options;
using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookRecommendation.Datalayer.MongoModel
{
    public class UserToUser
    {
        [BsonElement("_id")]
        public ObjectId _id { get; set; }
        [BsonElement("user_id")]
        public int User { get; set; }
        [BsonDictionaryOptions(DictionaryRepresentation.ArrayOfArrays)]
        public Dictionary<int, double> SimilarUser { get; set; }
    }
}
