using System;
using System.Collections.Generic;
using System.Text;
using System.Linq.Expressions;

namespace Todo.Data.Interfaces
{
    using Todo.Domain.Models;
    public interface IContext
    {        
        IContextConfiguration Configuration { get; }
        IUserRepository Users { get; }
        IListRepository Lists { get; }
        IListItemRepository ListItems { get; }
        IRepository<T> RepositoryFor<T>() where T : Model;
    }
}
