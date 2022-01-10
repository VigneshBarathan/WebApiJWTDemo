using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApiJWTdemo.Interface;
using WebApiJWTdemo.Model;

namespace WebApiJWTdemo.Controllers
{
    public class MembersController : ControllerBase
    {
        private readonly IJwtAuth jwtAuth;

        private readonly List<Membermodel> lstMember = new List<Membermodel>()
        {
            new Membermodel{Id=1, Name="Alan" },
            new Membermodel {Id=2, Name="Charlie" },
            new Membermodel{Id=3, Name="Michelle"}
        };
        public MembersController(IJwtAuth jwtAuth)
        {
            this.jwtAuth = jwtAuth;
        }

        // GET: api/<MembersController>
        [HttpGet]
        public IEnumerable<Membermodel> AllMembers()
        {
            return lstMember;
        }

        // GET api/<MembersController>/5
        [HttpGet("{id}")]
        public Membermodel MemberByid(int id)
        {
            return lstMember.Find(x => x.Id == id);
        }

        [AllowAnonymous]
        // POST api/<MembersController>
        [HttpPost("authentication")]
        public IActionResult Authentication([FromBody] UserCredentialmodel userCredential)
        {
            var token = jwtAuth.Authentication(userCredential.UserName, userCredential.Password);
            if (token == null)
                return Unauthorized();
            return Ok(token);
        }
    }
}
