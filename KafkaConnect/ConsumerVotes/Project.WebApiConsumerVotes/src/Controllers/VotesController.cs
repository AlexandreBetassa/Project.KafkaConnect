using Microsoft.AspNetCore.Mvc;
using Project.Domain.src.Contracts;
using Project.Domain.src.Entities;

namespace Project.WebApiVotes.src.Controllers
{
    [Route("api/[controller]/v1")]
    [ApiController]
    public class VotesController : ControllerBase
    {
        private readonly IServices<Vote> _svc;

        public VotesController(IServices<Vote> svc)
        {
            _svc = svc;
        }

        #region Post
        //Usado somente para teste de funcionamento do banco, a persistencia em si é efetuado pelo servico do Kafka rodando em segundo plano

        //[HttpPost(Name = "PostVote")]
        //public async Task<IActionResult> Post(Participants vote)
        //{
        //    var result = await _svc.Create(new Vote { participants = vote });
        //    return CreatedAtRoute(nameof(GetOneVote), new { id = result.Id }, vote);
        //}
        #endregion Post

        [HttpGet("{id}", Name = "GetOneVote")]
        public async Task<IActionResult> GetOneVote(int id)
        {
            var result = await _svc.GetOne(id);
            return result is null ? NotFound() : Ok(result);
        }

        [HttpGet(Name = "GetAllVotes")]
        public async Task<IActionResult> GetAllVotes()
        {
            var result = await _svc.GetAll();
            return result is null ? NotFound() : Ok(result);
        }
    }
}
