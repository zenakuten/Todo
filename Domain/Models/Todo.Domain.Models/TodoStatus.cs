using System;
using System.Collections.Generic;
using System.Text;

namespace Todo.Domain.Models
{
    /// <summary>
    /// List of statuses a Todo item can be in.
    /// </summary>
    public enum TodoStatus
    {
        /// <summary>
        /// Todo has not been started
        /// </summary>
        NotStarted,

        /// <summary>
        /// User has initiated Todo
        /// </summary>
        InProgress,

        /// <summary>
        /// User has completed Todo
        /// </summary>
        Complete
    }
}
