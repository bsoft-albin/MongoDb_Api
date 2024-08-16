using Microsoft.AspNetCore.Mvc;
using MongoDb_Api.Models.QuickChatModels;
using MongoDb_Api.Services.QuickChatService;

namespace MongoDb_Api.Controllers.QuickChat
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class UserProfileController(IRegistrationService service) : ControllerBase
    {
        private readonly IRegistrationService _service = service;

        [HttpPost]
        [ActionName("CreateUserAsync")]
        public async Task<IActionResult> CreateUser([FromBody] UserProfile userProfile)
        {
            if (ModelState.IsValid) {
                return Ok(await _service.CreateUserAccount(userProfile));
            }
            
            return BadRequest();
        }
    }
}
