using System;
using System.Collections.Generic;
using System.Text;

namespace Todo.Domain.Services
{
    using Todo.Domain.Models;
    using Todo.Domain.Interfaces;
    using Todo.Data.Interfaces;

    public class ListItemServices : CrudServices<ListItemModel>, IListItemServices
    {
        private IListItemRepository _listItemRepository;
        public ListItemServices(IListItemRepository listItemRepository) : base(listItemRepository)
        {
            _listItemRepository = listItemRepository;
        }

        public List<ListItemModel> GetByList(ListModel list)
        {
            return _listItemRepository.GetByList(list);
        }

        public int Owner(int listItemId)
        {
            var item = Read(listItemId);
            if (item == null)
                return 0;

            return item.ListId;
        }
        
        public int Owner(ListItemModel listItem)
        {
            if (listItem == null)
                return 0;

            return Owner(listItem.Id);
        }
    }
}
