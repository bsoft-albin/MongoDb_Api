using Microsoft.AspNetCore.Mvc;
using MongoDb_Api.Models.QuickChatModels;
using MongoDb_Api.Services.QuickChatService;

namespace MongoDb_Api.Controllers.QuickChat
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class CommonEndpointsController : ControllerBase
    {
        private readonly ICommonServices _commonServices;
        public CommonEndpointsController(ICommonServices commonServices)
        {
             _commonServices = commonServices;
        }

        [HttpGet]
        [ActionName("SearchQuickChatUsersAsync")]
        public async Task<ActionResult> SearchQuickChatUsers([FromQuery] string getValue)
        {
            return Ok(await _commonServices.GetUserNamesAndEmailsAsync(getValue));
        }
    }
}
