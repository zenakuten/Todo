using System;

namespace Todo.Domain.Models
{
    /// <summary>
    /// A Todo User
    /// </summary>
    public class UserModel : Model
    {
        public UserModel()
        {
        }
        
        /// <summary>
        /// API level identifier of a user
        /// </summary>
        public Guid UserGuid { get; set; }

        /// <summary>
        /// Username
        /// </summary>
        public string Username { get; set; }
    }
}
