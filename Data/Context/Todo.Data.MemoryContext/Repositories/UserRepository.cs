using System;
using System.Collections.Generic;
using System.Text;

namespace Todo.Data.Context.MemoryContext.Repositories
{
    using Todo.Domain.Models;
    using Todo.Data.Interfaces;
    using Todo.Data.Context.MemoryContext.Interfaces;
    using System.Linq;
    using System.Linq.Expressions;
    public class UserRepository : CrudRepository<UserModel>, IUserRepository
    {
        public UserRepository(IMemoryContext context) : base(context)
        {
        }

        public UserModel GetByUsername(string username)
        {
            return _memoryContext
                .StoreFor<UserModel>()
                .Where(u => string.Compare(u.Username, username, true) == 0)
                .FirstOrDefault();
        }
    }
}
