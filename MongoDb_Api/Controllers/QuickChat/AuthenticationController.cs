using Microsoft.AspNetCore.Mvc;
using MongoDb_Api.Models.QuickChatModels;
using MongoDb_Api.Services.QuickChatService;

namespace MongoDb_Api.Controllers.QuickChat
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class AuthenticationController(IRegistrationService registrationService) : ControllerBase
    {
        private readonly IRegistrationService _registrationService = registrationService;
        [HttpPost]
        [ActionName("GetUserDetailsAsync")]
        public async Task<ActionResult> GetUserDetails([FromBody] UserObject userObject)
        {
            if (ModelState.IsValid)
            {
                return Ok(await _registrationService.GetUserDetails(userObject));
            }

            return BadRequest();
        }

    }
}
