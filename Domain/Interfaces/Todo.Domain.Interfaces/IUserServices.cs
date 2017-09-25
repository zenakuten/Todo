using System;

namespace Todo.Domain.Interfaces
{
    using Todo.Domain.Models;

    /// <summary>
    /// User services
    /// </summary>
    public interface IUserServices : ICrudServices<UserModel>
    {
        UserModel GetByUsername(string username);
        UserModel Login(string username, string password);
    }
}
