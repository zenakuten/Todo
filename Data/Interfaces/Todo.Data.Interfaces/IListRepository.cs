using System;
using System.Collections.Generic;
using System.Text;

namespace Todo.Data.Interfaces
{
    using Todo.Domain.Models;
    public interface IListRepository : ICrudRepository<ListModel>
    {

        List<ListModel> GetByUserId(int userId);
    }
}
