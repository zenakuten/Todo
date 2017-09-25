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

    [Route("api/lists/{listId}/[controller]")]
    public class ListItemsController : Controller
    {
        private IListItemServices _listItemServices;
        public ListItemsController(IListItemServices listItemServices)
        {
            _listItemServices = listItemServices;
        }

        // GET api/lists/5
        [HttpGet("{id}")]
        public IActionResult Get(int listId, int id)
        {
            if (listId != _listItemServices.Owner(id))
                return BadRequest();

            var retval = _listItemServices.Read(id);
            if (retval != null)
            {
                return Ok(retval.ToApiModel());
            }

            return NotFound();
        }

        // POST api/lists
        [HttpPost]
        public IActionResult Post(int listId, [FromBody] ListItemApiModel value)
        {
            if (value == null)
                return BadRequest();

            var retval = _listItemServices.Save(value.ToDomainModel()).ToApiModel();
            return Created(Request.Path + $"/lists/{listId}/listitems/{retval.Id}", retval);
        }

        // PUT api/lists/5
        [HttpPut("{id}")]
        public IActionResult Put(int listId, int id, [FromBody]ListItemApiModel value)
        {
            if (listId != _listItemServices.Owner(id))
                return BadRequest();

            var retval = _listItemServices.Read(id);
            if (retval != null)
            {
                return Created(Request.Path, _listItemServices.Update(value.ToDomainModel()).ToApiModel());
            }

            return NotFound();
        }

        // DELETE api/lists/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int listId, int id)
        {
            if (listId != _listItemServices.Owner(id))
                return BadRequest();

            return Ok(_listItemServices.Delete(id));
        }
    }
}
