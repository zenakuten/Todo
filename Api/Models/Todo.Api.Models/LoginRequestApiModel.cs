using System;
using System.Collections.Generic;
using System.Text;

namespace Todo.Api.Models
{
    public class LoginRequestApiModel : ApiModel
    {
        public string username { get; set; }
        public string password { get; set; }
    }
}
