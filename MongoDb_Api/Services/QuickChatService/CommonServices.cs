using MongoDB.Bson;
using MongoDb_Api.Models.QuickChatModels;
using MongoDb_Api.Repositories.QuickChatRepo;

namespace MongoDb_Api.Services.QuickChatService
{
    public interface ICommonServices
    {
        Task<BaseResponse> GetUserNamesAndEmailsAsync(string name);
    }
    public class CommonServices : ICommonServices
    {
        private readonly ICommonRepo _commonRepo;
        public CommonServices(ICommonRepo commonRepo)
        {
            _commonRepo = commonRepo;
        }

        public async Task<BaseResponse> GetUserNamesAndEmailsAsync(string name)
        {
            BaseResponse baseResponse = new();
            List<UserSearch> lists = await _commonRepo.GetUserNamesAndEmailsAsync(name);

            if (lists != null && lists.Count > 0)
            {
                baseResponse.Data = lists;
                baseResponse.StatusCode = 200;
            }
            else {
                baseResponse.StatusCode = 204;
                baseResponse.StatusMessage = "No record found";
            }

            return baseResponse;
        }
    }
}
