using System;
using System.Collections.Generic;
using System.Text;

namespace Todo.Domain.Models
{
    /// <summary>
    /// The base model all others derive from
    /// </summary>
    public class Model
    {
        public Model()
        {
        }

        /// <summary>
        /// Unique identifier for the model
        /// </summary>
        public int Id { get; set; }
    }
}
