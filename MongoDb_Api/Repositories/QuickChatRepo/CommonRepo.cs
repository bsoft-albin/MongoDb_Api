using MongoDB.Bson;
using MongoDB.Driver;
using MongoDb_Api.DbEngine;
using MongoDb_Api.Frameworks.CommonMeths;
using MongoDb_Api.Models.QuickChatModels;

namespace MongoDb_Api.Repositories.QuickChatRepo
{

    public interface ICommonRepo
    {
        Task<List<UserSearch>> GetUserNamesAndEmailsAsync(string inputText);
    }

    public class CommonRepo : ICommonRepo
    {
        private readonly IMongoCloudEngine _mongoCloud;
        public CommonRepo(IMongoCloudEngine mongoCloud)
        {
            _mongoCloud = mongoCloud;
        }

        // projection technique for getting only specific columns like in RDBMS.
        public async Task<List<UserSearch>> GetUserNamesAndEmailsAsync(string inputText)
        {
            List<UserSearch> list = [];
            try
            {
                // Get the collection
                var result = await _mongoCloud.GetRawDocumentsAsListAsync<UserSearch>("users");

                // Create a filter to match documents where the 'username' field contains the input text
                var filter = Builders<UserSearch>.Filter.Or(
                    Builders<UserSearch>.Filter.Regex("username", new BsonRegularExpression(inputText, "i")),
                    Builders<UserSearch>.Filter.Regex("name", new BsonRegularExpression(inputText, "i"))
                );

                var projection = Builders<UserSearch>.Projection
                                                     .Include(u => u.UserName)
                                                     .Include(u => u.Email)
                                                     .Include(u => u.Id).Include(u => u.Name);

                // Fetch documents that match the filter and apply the projection
                list = await result.Find(filter)
                                            .Project<UserSearch>(projection)  // Project fields onto the UserSearch DTO
                                            .ToListAsync();
            }
            catch (Exception x)
            {
                await ErrorLogger.WriteLog(x);
            }

            return list;
        }
    }
}
