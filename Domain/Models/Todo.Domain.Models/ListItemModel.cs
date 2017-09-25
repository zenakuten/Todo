using System;
using System.Collections.Generic;
using System.Text;

namespace Todo.Domain.Models
{
    /// <summary>
    /// List item data
    /// </summary>
    public class ListItemModel : Model
    {
        public ListItemModel()
        {
            //default to sane values
            Value = string.Empty;
            Details = string.Empty;
            Deadline = DateTime.MaxValue;
            Completed = false;
        }

        /// <summary>
        /// The Id of the list this list item belongs to
        /// </summary>
        public int ListId { get; set; }

        /// <summary>
        /// The text value of the Todo list
        /// </summary>
        public string Value { get; set; }

        /// <summary>
        /// The text value of the Todo list
        /// </summary>
        public string Details { get; set; }

        /// <summary>
        /// Time date/time when the Todo must be completed
        /// </summary>
        public DateTime Deadline { get; set; }

        /// <summary>
        /// is the todo completed?
        /// </summary>
        public bool Completed { get; set; }
    }
}
