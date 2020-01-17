using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MoviesApp.Domain;
using MoviesApp.BLL;
using Microsoft.Extensions.Logging;
namespace MoviesApp.API.Controllers
{
    [Route("votes")]
    [ApiController]
    public class VoteController : ControllerBase
    {
        private VoteService _service;
        private readonly ILogger<VoteController> _logger;

        public VoteController(ILogger<VoteController> logger, VoteService service)
        {
            _logger = logger;
            _service = service;
        }

        [HttpGet]
        public IEnumerable<VoteDTO> GetVotes()
        {
            return _service.GetVotes().Select(v => v.ToVoteDTO()).ToList();
        }

        [HttpGet]
        [Route("{memberId}/{movieId}")]
        public VoteDTO GetVote(int memberId, int movieId)
        {
            return _service.GetVote((memberId, movieId)).ToVoteDTO();
        }

        [HttpPost]
        public ActionResult PostVote(VoteDTO voteDTO)
        {
            _service.AddVote(voteDTO.ToVote());
            return Ok();
        }

        [HttpDelete]
        [Route("{memberId}/{movieId}")]
        public ActionResult DeleteVote(int memberId, int movieId)
        {
            _service.DeleteVote((memberId, movieId));
            return Ok();
        }

        [HttpPut]
        public ActionResult UpdateVote(VoteDTO voteDTO)
        {
            _service.UpdateVote(voteDTO.ToVote());
            return Ok();
        }
    }
}

