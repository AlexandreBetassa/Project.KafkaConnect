using Microsoft.AspNetCore.Mvc;
using Project.Contracts.src;
using Project.Models.src.Entities;
using Project.Models.src.Enum;

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
            return Ok(_service.Create(vote));
        }
    }
}
