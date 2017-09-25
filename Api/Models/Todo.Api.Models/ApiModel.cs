using System;
using System.Collections.Generic;
using System.Text;

namespace Todo.Api.Models
{
    using Newtonsoft.Json;
    public class ApiModel
    {
        public string ToJson()
        {
            return JsonConvert.SerializeObject(this);
        }
    }
}
