namespace MongoDb_Api.Models.QuickChatModels
{
    public class BaseResponse
    {
        public Object? Data { get; set; }
        public int StatusCode { get; set; } = 200;
        public String StatusMessage { get; set; } = "Success";
    }
}
