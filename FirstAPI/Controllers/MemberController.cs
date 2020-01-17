using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MoviesApp.BLL;
using MoviesApp.Domain;
using Microsoft.Extensions.Logging;

namespace MoviesApp.API.Controllers
{
    [Route("members")]
    [ApiController]
    public class MemberController : ControllerBase
    {
        private MemberService _service;
        private readonly ILogger<MemberController> _logger;

        public MemberController(ILogger<MemberController> logger, MemberService service)
        {
            _logger = logger;
            _service = service;
        }

        [HttpGet]
        public IEnumerable<Member> GetMembers()
        {
            return _service.GetMembers();
        }

        [HttpGet("{id}")]
        public Member GetMember(int id)
        {
            return _service.GetMember(id);
        }

        [HttpPost]
        public ActionResult PostMember([FromBody] Member member)
        {
            _service.AddMember(member);
            return Ok();
        }

        [HttpPut]
        public ActionResult PutMember([FromBody] Member member)
        {
            _service.UpdateMember(member);
            return Ok();
        }

        [HttpDelete("{id}")]
        public ActionResult Delete(int id)
        {
            _service.DeleteMember(id);
            return Ok();
        }
    }
}
