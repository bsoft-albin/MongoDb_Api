using MongoDB.Bson;
using MongoDb_Api.DbEngine;
using MongoDb_Api.Models.QuickChatModels;

namespace MongoDb_Api.Repositories.QuickChatRepo
{
    public interface IRegistrationRepo
    {
        Task<String> CreateUserAccount(UserProfile userProfile);
    }
    public class RegistrationRepo(IMongoCloudEngine cloudEngine) : IRegistrationRepo
    {
        private readonly IMongoCloudEngine _cloudEngine = cloudEngine;
        private const string userCollection = "users";
        public async Task<String> CreateUserAccount(UserProfile userProfile)
        {
            String id = "";
            try
            {
                id = await _cloudEngine.CreateDocumentAsync<UserProfile>(userCollection, userProfile);
            }
            catch (Exception ex)
            {

            }
            
            return id;
        }
    }
}
