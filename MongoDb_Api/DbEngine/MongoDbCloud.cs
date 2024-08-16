using MongoDB.Bson;
using MongoDB.Driver;

namespace MongoDb_Api.DbEngine
{
    public interface IMongoCloudEngine
    {
        Task<String> CreateDocumentAsync<T>(string collectionName, T document);
        Task<T> GetDocumentByIdAsync<T>(string collectionName, string id);
        Task<UpdateResult> UpdateDocumentByIdAsync<T>(string collectionName, string id, T document);
        Task<DeleteResult> DeleteDocumentByIdAsync<T>(string collectionName, string id);
        Task<List<X>> GetAllDocumentsAsync<X>(string collectionName);
    }

    public class MongoCloudEngine : IMongoCloudEngine
    {
        private readonly IMongoDatabase _mongoDatabase;

        public MongoCloudEngine(string ConString, string DbName)
        {
            var client = new MongoClient(ConString);
            _mongoDatabase = client.GetDatabase(DbName);
        }

        public async Task<String> CreateDocumentAsync<T>(string collectionName, T document)
        {
            IMongoCollection<T> collection;
            try
            {
                collection = _mongoDatabase.GetCollection<T>(collectionName);
                await collection.InsertOneAsync(document);

                // Assuming the document has an ObjectId property named "_id".
                var objectId = typeof(T).GetProperty("_id")?.GetValue(document);

                #pragma warning disable
                return objectId.ToString();
            }
            catch (Exception ex)
            {

                throw;
            }
        }

        public async Task<DeleteResult> DeleteDocumentByIdAsync<T>(string collectionName, string id)
        {
            IMongoCollection<T> collection;
            DeleteResult delresult;
            try
            {
                collection = _mongoDatabase.GetCollection<T>(collectionName);
                FilterDefinition<T> filter = Builders<T>.Filter.Eq("_id", ObjectId.Parse(id));
                delresult = await collection.DeleteOneAsync(filter);
            }
            catch (Exception ex)
            {

                throw;
            }
            
            return delresult;
        }

        public async Task<List<X>> GetAllDocumentsAsync<X>(string collectionName)
        {
            try
            {
                var collection = _mongoDatabase.GetCollection<X>(collectionName);
                return await collection.Find(_ => true).ToListAsync();
            }
            catch (Exception ex)
            {

                throw;
            }
            
        }

        public async Task<T> GetDocumentByIdAsync<T>(string collectionName, string id)
        {
            try
            {
                var collection = _mongoDatabase.GetCollection<T>(collectionName);
                var filter = Builders<T>.Filter.Eq("_id", ObjectId.Parse(id));
                return await collection.Find(filter).FirstOrDefaultAsync();
            }
            catch (Exception ex)
            {

                throw;
            }
            
        }

        public async Task<UpdateResult> UpdateDocumentByIdAsync<T>(string collectionName, string id, T document)
        {
            try
            {
                var collection = _mongoDatabase.GetCollection<T>(collectionName);
                var filter = Builders<T>.Filter.Eq("_id", ObjectId.Parse(id));

                // Convert the document to BsonDocument to manipulate its fields
                var bsonDocument = document.ToBsonDocument();

                // Remove the _id field to prevent modification of the immutable _id
                bsonDocument.Remove("_id");

                // Define the update operation using $set
                var updateDefinition = new BsonDocument("$set", bsonDocument);

                // Perform the update
                return await collection.UpdateOneAsync(filter, updateDefinition);
            }
            catch (Exception ex)
            {

                throw;
            }
            
        }

        public async Task<X> GetFilterDocumentAsync<X>(string collectionName, FilterDefinition<X> filter) where X : new()
        {
            try
            {
                var collection = _mongoDatabase.GetCollection<X>(collectionName);
                return await collection.Find(filter).FirstOrDefaultAsync();
            }
            catch (Exception ex)
            {
                return new();
            }
        }
    }
}
