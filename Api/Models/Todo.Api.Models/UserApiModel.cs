using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Todo.Api.Models
{
    public class UserApiModel : ApiModel
    {
        public int Id { get; set; }
        public string UserGuid { get; set; }
        public string Username { get; set; }
    }
}