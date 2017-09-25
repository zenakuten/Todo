using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Todo.Api.Models
{
    public class ListItemApiModel : ApiModel
    {
        public ListItemApiModel()
        {
            //default to sane values
            Value = string.Empty;
            Details = string.Empty;
            Deadline = DateTime.MaxValue;
            Completed = false;
        }

        /// <summary>
        /// Item Id
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// List Id of list that owns this item
        /// </summary>
        public int ListId { get; set; }

        /// <summary>
        /// The text value of the Todo list item
        /// </summary>
        public string Value { get; set; }

        /// <summary>
        /// The details of the todo list item
        /// </summary>
        public string Details { get; set; }

        /// <summary>
        /// Time date/time when the Todo must be completed
        /// </summary>
        public DateTime Deadline { get; set; }

        /// <summary>
        /// The current status of the Todo
        /// </summary>
        public bool Completed { get; set; }

    }
}