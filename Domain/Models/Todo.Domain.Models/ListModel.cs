using System;
using System.Collections.Generic;
using System.Text;

namespace Todo.Domain.Models
{
    /// <summary>
    /// Container for todo list items
    /// </summary>    
    public class ListModel : Model
    {
        public ListModel()
        {
            //Collections on domain models should default to empty, not null
            Items = new List<ListItemModel>();
        }

        //owner of this list
        public int UserId { get; set; }

        //name of the list
        public string Name { get; set; }
        
        //The list of items
        public List<ListItemModel> Items { get; private set; }
    }
}
