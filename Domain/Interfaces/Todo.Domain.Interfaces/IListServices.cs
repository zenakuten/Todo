using System;
using System.Collections.Generic;
using System.Text;

namespace Todo.Domain.Interfaces
{
    using Todo.Domain.Models;
    public interface IListServices : ICrudServices<ListModel>
    {
        List<ListModel> GetByUserId(int userId);
    }
}
