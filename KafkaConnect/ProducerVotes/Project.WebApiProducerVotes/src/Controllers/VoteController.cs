using Microsoft.AspNetCore.Mvc;
using Project.Domain.src.Contracts;
using Project.Domain.src.Entities;
using Project.Domain.src.Enum;

namespace ProducerApi.src.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VoteController : ControllerBase
    {
        private readonly IServices<Vote> _service;
        public VoteController(IServices<Vote> service)
        {
            _service = service;
        }

        [HttpPost(Name = "Post")]
        public async Task<IActionResult> Post(Participants participant)
        {
            var vote = new Vote { Participants = participant };
            return _service.Create(vote).IsCompletedSuccessfully ? Ok("Send Success") : BadRequest("Error");
        }

        [HttpGet(Name = "Get")]
        public async Task<IActionResult> Get()
        {
            return Ok("");
        }
    }
}
