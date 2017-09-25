using System;
using System.Collections.Generic;
using System.Text;

namespace Todo.Data.Interfaces
{
    using Todo.Domain.Models;
    public interface IUserRepository : ICrudRepository<UserModel>
    {
        UserModel GetByUsername(string username);
    }
}
