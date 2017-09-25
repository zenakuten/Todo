using System;
using System.Collections.Generic;
using System.Text;

namespace Todo.Data.Context.MemoryContext.Repositories
{
    using System.Linq;
    using Todo.Domain.Models;
    using Todo.Data.Interfaces;
    using Todo.Data.Context.MemoryContext.Interfaces;
    public class ListRepository : CrudRepository<ListModel>, IListRepository
    {
        public ListRepository(IMemoryContext context) : base(context)
        {
        }

        public List<ListModel> GetByUserId(int userId)
        {
            return _memoryContext.StoreFor<ListModel>().Where(s => s.UserId == userId).ToList();
        }

    }
}
