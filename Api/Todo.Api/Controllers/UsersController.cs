using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace Todo.Api.Controllers
{
    using Todo.Domain.Interfaces;
    using Todo.Api.Models;
    using Todo.Api.Mappers;

    [Route("api/[controller]")]
    public class UsersController : Controller
    {
        private IUserServices _userServices;
        private IListServices _listServices;
        public UsersController(IUserServices userServices, IListServices listServices)
        {
            _userServices = userServices;
            _listServices = listServices;
        }

        // GET api/users/5
        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            var retval = _userServices.Read(id);
            if (retval != null)
            {
                return Ok(retval.ToApiModel());
            }

            return NotFound();
        }

        // POST api/users
        [HttpPost]
        public IActionResult Post([FromBody] UserApiModel value)
        {
            if (value == null)
                return BadRequest();

            var retval = _userServices.Create(value.ToDomainModel()).ToApiModel();
            if (retval == null)
                return NotFound();

            return Created(Request.Path + $"/{retval.Id}", retval);
        }

        // PUT api/users/5
        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody]UserApiModel value)
        {
            var retval = _userServices.Read(id);
            if (retval != null)
            {
                return Created(Request.Path,_userServices.Update(value.ToDomainModel()).ToApiModel());
            }

            return NotFound();
        }

        // DELETE api/users/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            return Ok(_userServices.Delete(id));
        }

        [HttpPost("login")]
        public IActionResult Login([FromBody] LoginRequestApiModel loginRequest)
        {
            if (loginRequest == null)
                return BadRequest();

            var retval = _userServices.Login(loginRequest.username, loginRequest.password);
            if (retval == null)
                return Unauthorized();


            return Created(Request.PathBase, retval.ToApiModel());
        }

        [HttpGet("{id}/lists")]
        public IActionResult Lists(int id)
        {

            var retval = _listServices.GetByUserId(id);
            if (retval == null)
                return NotFound();

            return Ok(retval.Select( list => list.ToApiModel()).ToArray());
        }
    }
}
