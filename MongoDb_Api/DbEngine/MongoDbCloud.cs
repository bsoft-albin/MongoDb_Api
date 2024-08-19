using MongoDB.Bson;
using MongoDB.Driver;
using MongoDb_Api.Frameworks.CommonMeths;

namespace MongoDb_Api.DbEngine
{
    public interface IMongoCloudEngine
    {
        Task<String> CreateDocumentAsync<T>(string collectionName, T document);
        Task<T> GetDocumentByIdAsync<T>(string collectionName, string id);
        Task<UpdateResult> UpdateDocumentByIdAsync<T>(string collectionName, string id, T document);
        Task<DeleteResult> DeleteDocumentByIdAsync<T>(string collectionName, string id);
        Task<IMongoCollection<X>> GetAllDocumentsAsync<X>(string collectionName);
        Task<List<X>> GetAllDocumentsAsListAsync<X>(string collectionName);
        Task<X> GetFilterDocumentAsync<X>(string collectionName, FilterDefinition<X> filter);
    }

    public class MongoCloudEngine : IMongoCloudEngine
    {
        private readonly IMongoDatabase _mongoDatabase;

        public MongoCloudEngine(string ConString, string DbName)
        {
            MongoClient client = new(ConString);
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
                await ErrorLogger.WriteLog(ex);
                throw;
            }
        }

        public async Task<DeleteResult> DeleteDocumentByIdAsync<T>(string collectionName, string id)
        {
            IMongoCollection<T> collection;
            try
            {
                collection = _mongoDatabase.GetCollection<T>(collectionName);
                FilterDefinition<T> filter = Builders<T>.Filter.Eq("_id", ObjectId.Parse(id));
                return await collection.DeleteOneAsync(filter);
            }
            catch (Exception ex)
            {
                await ErrorLogger.WriteLog(ex);
                throw;
            }
        }

        public async Task<IMongoCollection<X>> GetAllDocumentsAsync<X>(string collectionName)
        {
            try
            {
                return _mongoDatabase.GetCollection<X>(collectionName);
            }
            catch (Exception ex)
            {
                await ErrorLogger.WriteLog(ex);
                throw;
            }
            
        }

        public async Task<List<X>> GetAllDocumentsAsListAsync<X>(string collectionName)
        {
            try
            {
                IMongoCollection<X> mongoCollection = _mongoDatabase.GetCollection<X>(collectionName);
                return await mongoCollection.Find(_ => true).ToListAsync();
            }
            catch (Exception ex)
            {
                await ErrorLogger.WriteLog(ex);
                throw;
            }

        }

        public async Task<T> GetDocumentByIdAsync<T>(string collectionName, string id)
        {
            try
            {
                IMongoCollection<T> collection = _mongoDatabase.GetCollection<T>(collectionName);
                FilterDefinition<T> filter = Builders<T>.Filter.Eq("_id", ObjectId.Parse(id));
                return await collection.Find(filter).FirstOrDefaultAsync();
            }
            catch (Exception ex)
            {
                await ErrorLogger.WriteLog(ex);
                throw;
            }
            
        }

        public async Task<UpdateResult> UpdateDocumentByIdAsync<T>(string collectionName, string id, T document)
        {
            try
            {
                IMongoCollection<T> collection = _mongoDatabase.GetCollection<T>(collectionName);
                FilterDefinition<T> filter = Builders<T>.Filter.Eq("_id", ObjectId.Parse(id));

                // Convert the document to BsonDocument to manipulate its fields
                BsonDocument bsonDocument = document.ToBsonDocument();

                // Remove the _id field to prevent modification of the immutable _id
                bsonDocument.Remove("_id");

                // Define the update operation using $set
                BsonDocument updateDefinition = new BsonDocument("$set", bsonDocument);

                // Perform the update
                return await collection.UpdateOneAsync(filter, updateDefinition);
            }
            catch (Exception ex)
            {
                await ErrorLogger.WriteLog(ex);
                throw;
            }
            
        }

        public async Task<X> GetFilterDocumentAsync<X>(string collectionName, FilterDefinition<X> filter)
        {
            // here X => can be IMongoCollection or List<type> or (most probably)Single type
            try
            {
                IMongoCollection<X> collection = _mongoDatabase.GetCollection<X>(collectionName);

                return await collection.Find(filter).FirstOrDefaultAsync();
            }
            catch (Exception ex)
            {
                await ErrorLogger.WriteLog(ex);
                throw;
            }
        }
    }
}
