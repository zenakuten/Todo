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
    public class ListsController : Controller
    {
        private IListServices _listServices;
        public ListsController(IListServices listServices)
        {
            _listServices = listServices;
        }

        // GET api/lists/5
        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            var retval = _listServices.Read(id);
            if (retval != null)
            {
                return Ok(retval.ToApiModel());
            }

            return NotFound();
        }

        // POST api/lists
        [HttpPost]
        public IActionResult Post([FromBody] ListApiModel value)
        {
            if (value == null)
                return BadRequest();

            var retval = _listServices.Save(value.ToDomainModel()).ToApiModel();
            return Created(Request.Path + $"/{retval.Id}", retval);
        }

        // PUT api/lists/5
        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody]ListApiModel value)
        {
            var retval = _listServices.Read(id);
            if (retval != null)
            {
                return Created(Request.Path, _listServices.Update(value.ToDomainModel()).ToApiModel());
            }

            return NotFound();
        }

        // DELETE api/lists/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            return Ok(_listServices.Delete(id));
        }
    }
}
