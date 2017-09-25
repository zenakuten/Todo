using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Todo.Api.Models
{
    public class ListApiModel : ApiModel
    {
        public ListApiModel()
        {
            Items = new ListItemApiModel[0];
        }
        public int Id { get; set; }
        public int UserId { get; set; }
        public string Name { get; set; }
        public ListItemApiModel[] Items { get; set; }
    }
}