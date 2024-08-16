using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;
using MongoDb_Api.Models;
using MongoDb_Api.Services;

namespace MongoDb_Api.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class MongoDbApiController : ControllerBase
    {
        private readonly IMongoSampleService _mongoSample;
        public MongoDbApiController(IMongoSampleService mongoSample)
        {
            _mongoSample = mongoSample;
        }

        [HttpGet]
        [ActionName("GetRecordByIdAsync")]
        public async Task<IActionResult> GetRecordById([FromQuery] string collectionName, string id) {

            return Ok(await _mongoSample.GetByIdAsync<UserModel>(collectionName, id));
        }

        [HttpPost]
        [ActionName("PostRecordAsync")]
        public async Task<IActionResult> PostRecord([FromBody] UserModel userModel, string id)
        {
            await _mongoSample.CreateAsync<UserModel>(id, userModel);

            return Ok(StatusCodes.Status201Created);
        }

        [HttpPost]
        [ActionName("PutRecordAsync")]
        public async Task<IActionResult> PutRecord([FromBody] UserModel userModel, string id, string collectionName)
        {
            await _mongoSample.UpdateAsync<UserModel>(collectionName, id, userModel);

            return Ok(StatusCodes.Status202Accepted);
        }

        [HttpGet]
        [ActionName("GetRecordsAsync")]
        public async Task<IActionResult> GetAllRecords([FromQuery] string collectionName)
        {

            return Ok(await _mongoSample.GetAllRecords<UserModel>(collectionName));
        }

        [HttpGet]
        [ActionName("DeleteRecordAsync")]
        public async Task<IActionResult> DeleteRecordById([FromQuery] string collectionName, string id)
        {
            await _mongoSample.DeleteAsync<UserModel>(collectionName, id);
            return Ok(StatusCodes.Status204NoContent);
        }

    }
}
