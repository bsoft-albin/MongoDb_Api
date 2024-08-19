using MongoDB.Driver;
using MongoDb_Api.DbEngine;
using MongoDb_Api.Frameworks.CommonMeths;
using MongoDb_Api.Models.QuickChatModels;

namespace MongoDb_Api.Repositories.QuickChatRepo
{
    public interface IRegistrationRepo
    {
        Task<String> CreateUserAccount(UserProfile userProfile);
        Task<UserProfile> GetUserDetails(UserObject userObject);
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
                await ErrorLogger.WriteLog(ex);
            }
            
            return id;
        }

        public async Task<UserProfile> GetUserDetails(UserObject userObject)
        {
            UserProfile user = new();
            // Create the filter for username and password
            FilterDefinition<UserProfile> filter = Builders<UserProfile>.Filter.And(
                Builders<UserProfile>.Filter.Eq(u => u.UserName, userObject.UserName),
                Builders<UserProfile>.Filter.Eq(u => u.Password, userObject.Password)
            );
            try
            {
                user = await _cloudEngine.GetFilterDocumentAsync(userCollection, filter);
            }
            catch (Exception ex)
            {
                await ErrorLogger.WriteLog(ex);
            }
            
            return user;
        }
    }
}
