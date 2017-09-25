using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Todo.Api
{
    using Todo.Data.Context;
    public class ApiContextConfiguration : ContextConfiguration
    {
        public ApiContextConfiguration()
        {
            Settings["DataFile"] = "TestData.json";
        }
    }
}