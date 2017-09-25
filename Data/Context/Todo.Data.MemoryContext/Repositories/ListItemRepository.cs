using System;
using System.Collections.Generic;
using System.Text;

namespace Todo.Data.Context.MemoryContext.Repositories
{
    using Todo.Domain.Models;
    using Todo.Data.Interfaces;
    using Todo.Data.Context.MemoryContext.Interfaces;
    public class ListItemRepository : CrudRepository<ListItemModel>, IListItemRepository
    {
        public ListItemRepository(IMemoryContext context) : base(context)
        {
        }

        public List<ListItemModel> GetByList(ListModel list)
        {
            return EntitySet.FindAll(es => es.ListId == list.Id);
        }
    }
}
