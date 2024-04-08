using MongoDB.Driver;

namespace DataLayer
{
    public class MongoDataRepository
    {
        public MongoDataRepository(string connectionString, string database)
        { 
            client = new MongoClient(connectionString);
            clientDatabase = client.GetDatabase(database);
        }

        public MongoClient client {  get; set; }
        public IMongoDatabase clientDatabase { get; set; }

        public IMongoCollection<T> GetCollection<T>(string collectionName) 
        {
            var collection = clientDatabase.GetCollection<T>(collectionName);
            return collection;
        }

        public void CreateCollection(string name)
        {
            clientDatabase.CreateCollection(name);
        }

        public async void AppendToCollection<T>(string collectionName, IEnumerable<T> collection) 
        {
            await clientDatabase.GetCollection<T>(collectionName).InsertManyAsync(collection);
        }
    }
}
