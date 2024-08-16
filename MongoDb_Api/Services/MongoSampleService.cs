using MongoDB.Bson;
using MongoDB.Driver;
using MongoDb_Api.Models;

namespace MongoDb_Api.Services
{
    public interface IMongoSampleService
    {
        Task CreateAsync<T>(string collectionName, T document);
        Task<T> GetByIdAsync<T>(string collectionName, string id);
        Task UpdateAsync<T>(string collectionName, string id, T document);
        Task DeleteAsync<T>(string collectionName, string id);
        Task<List<UserModel>> GetAllRecords<T>(string collectionName);
    }
    public class MongoSampleService : IMongoSampleService
    {
        private readonly IMongoDatabase _database;

        public MongoSampleService(string ConString, string DbName)
        {
            var client = new MongoClient(ConString);
            _database = client.GetDatabase(DbName);
        }

        // Create
        public async Task CreateAsync<T>(string collectionName, T document)
        {
            var collection = _database.GetCollection<T>(collectionName);
            await collection.InsertOneAsync(document);
        }

        // Read by Id
        public async Task<T> GetByIdAsync<T>(string collectionName, string id)
        {
            var collection = _database.GetCollection<T>(collectionName);
            var filter = Builders<T>.Filter.Eq("_id", ObjectId.Parse(id));
            return await collection.Find(filter).FirstOrDefaultAsync();
        }

        // Update
        public async Task UpdateAsync<T>(string collectionName, string id, T document)
        {
            var collection = _database.GetCollection<T>(collectionName);
            var filter = Builders<T>.Filter.Eq("_id", ObjectId.Parse(id));

            // Convert the document to BsonDocument to manipulate its fields
            var bsonDocument = document.ToBsonDocument();

            // Remove the _id field to prevent modification of the immutable _id
            bsonDocument.Remove("_id");

            // Define the update operation using $set
            var updateDefinition = new BsonDocument("$set", bsonDocument);

            // Perform the update
            await collection.UpdateOneAsync(filter, updateDefinition);
        }

        // Delete
        public async Task DeleteAsync<T>(string collectionName, string id)
        {
            var collection = _database.GetCollection<T>(collectionName);
            var filter = Builders<T>.Filter.Eq("_id", ObjectId.Parse(id));
            await collection.DeleteOneAsync(filter);
        }

        //get all records
        public async Task<List<UserModel>> GetAllRecords<T>(string collectionName)
        {
            var collection = _database.GetCollection<UserModel>(collectionName);
            return await collection.Find(_ => true).ToListAsync();
        }

    }
}
