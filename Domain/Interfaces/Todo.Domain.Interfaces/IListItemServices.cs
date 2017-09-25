using System;
using System.Collections.Generic;
using System.Text;

namespace Todo.Domain.Interfaces
{
    using Todo.Domain.Models;
    public interface IListItemServices : ICrudServices<ListItemModel>
    {
        List<ListItemModel> GetByList(ListModel list);
        int Owner(int listItemId);
        int Owner(ListItemModel listItem);
    }
}
