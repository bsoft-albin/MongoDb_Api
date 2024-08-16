using Microsoft.AspNetCore.Mvc;
using MongoDb_Api.Models.QuickChatModels;

namespace MongoDb_Api.Controllers.QuickChat
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {
        [HttpPost]
        [ActionName("GetUserDetailsAsync")]
        public async Task<ActionResult> GetUserDetails([FromBody] UserObject userObject) {

            return Ok(StatusCodes.Status201Created);
        }

    }
}
