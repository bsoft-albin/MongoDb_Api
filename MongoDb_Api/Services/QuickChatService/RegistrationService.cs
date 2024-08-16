using MongoDB.Driver;
using MongoDb_Api.Models.QuickChatModels;
using MongoDb_Api.Repositories.QuickChatRepo;

namespace MongoDb_Api.Services.QuickChatService
{

    public interface IRegistrationService
    {
        Task<BaseResponse> CreateUserAccount(UserProfile userProfile);
        Task<BaseResponse> GetUserDetails(UserObject userObject);
    }

    public class RegistrationService(IRegistrationRepo repo) : IRegistrationService
    {
        private readonly IRegistrationRepo _repo = repo;
        public async Task<BaseResponse> CreateUserAccount(UserProfile userProfile)
        {
            BaseResponse baseResponse = new();
            String id = await _repo.CreateUserAccount(userProfile);

            if (id != null && id != "")
            {
                baseResponse.StatusMessage = "User Account Created";
                baseResponse.StatusCode = 201;
                baseResponse.Data = id;
            }
            else {
                baseResponse.StatusMessage = "Process Failed";
                baseResponse.StatusCode = 500;
            }

            return baseResponse;
        }

        public Task<BaseResponse> GetUserDetails(UserObject userObject)
        {
            throw new NotImplementedException();
        }
    }

}
