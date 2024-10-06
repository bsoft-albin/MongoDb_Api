using MongoDB.Bson;
using MongoDb_Api.Models.QuickChatModels;
using MongoDb_Api.Repositories.QuickChatRepo;

namespace MongoDb_Api.Services.QuickChatService
{
    public interface ICommonServices
    {
        Task<BaseResponse> GetUserNamesAndEmailsAsync();
    }
    public class CommonServices : ICommonServices
    {
        private readonly ICommonRepo _commonRepo;
        public CommonServices(ICommonRepo commonRepo)
        {
            _commonRepo = commonRepo;
        }

        public async Task<BaseResponse> GetUserNamesAndEmailsAsync()
        {
            BaseResponse baseResponse = new();
            List<BsonDocument> lists = await _commonRepo.GetUserNamesAndEmailsAsync();

            if (lists != null && lists.Count > 0)
            {
                baseResponse.Data = lists;
                baseResponse.StatusCode = 200;
            }
            else {
                baseResponse.StatusCode = 200;
                baseResponse.StatusMessage = "No record found";
            }

            return baseResponse;
        }
    }
}
