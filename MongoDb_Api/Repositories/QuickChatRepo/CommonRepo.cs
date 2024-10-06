using MongoDB.Bson;
using MongoDB.Driver;
using MongoDb_Api.DbEngine;
using MongoDb_Api.Frameworks.CommonMeths;
using MongoDb_Api.Models.QuickChatModels;

namespace MongoDb_Api.Repositories.QuickChatRepo
{

    public interface ICommonRepo
    {
        Task<List<BsonDocument>> GetUserNamesAndEmailsAsync();
    }

    public class CommonRepo : ICommonRepo
    {
        private readonly IMongoCloudEngine _mongoCloud;
        public CommonRepo(IMongoCloudEngine mongoCloud)
        {
            _mongoCloud = mongoCloud;
        }

        // projection technique for getting only specific columns like in RDBMS.
        public async Task<List<BsonDocument>> GetUserNamesAndEmailsAsync()
        {
            var result = new List<BsonDocument>();
            try
            {
                var _userProfiles = await _mongoCloud.GetRawDocumentsAsListAsync<UserProfile>("users");
                // Define the projection to include only 'UserName' and 'Email'
                ProjectionDefinition<UserProfile> projection = Builders<UserProfile>.Projection
                                .Include(u => u.UserName)
                                .Include(u => u.Email).Include(u => u._id);
                // .Exclude(u => u._id);  // Optionally exclude the _id field

                // Query the collection and apply the projection
                result = await _userProfiles.Find(new BsonDocument())
                                    .Project(projection)
                                    .ToListAsync();
            }
            catch (Exception x)
            {
                await ErrorLogger.WriteLog(x);
            }


            return result;
        }
    }
}
