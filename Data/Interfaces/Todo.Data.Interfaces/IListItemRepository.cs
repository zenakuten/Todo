using System;
using System.Collections.Generic;
using System.Text;

namespace Todo.Data.Interfaces
{
    using Todo.Domain.Models;
    public interface IListItemRepository : ICrudRepository<ListItemModel>
    {
        List<ListItemModel> GetByList(ListModel list);
    }
}
