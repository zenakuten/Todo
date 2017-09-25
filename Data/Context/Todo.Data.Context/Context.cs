using System;

namespace Todo.Data.Context
{
    using System.Collections.Generic;
    using Todo.Data.Interfaces;
    using Todo.Domain.Models;

    public abstract class Context : IContext
    {
        public abstract IUserRepository Users { get; }
        public abstract IListRepository Lists { get; }
        public abstract IListItemRepository ListItems { get; }

        private IContextConfiguration _configuration;
        private Dictionary<Type, object> _repoSet;
        public Context(IContextConfiguration configuration)
        {
            _configuration = configuration;
            _repoSet = new Dictionary<Type, object>();
            _repoSet.Add(typeof(UserModel), Users);
            _repoSet.Add(typeof(ListModel), Lists);
            _repoSet.Add(typeof(ListItemModel), ListItems);
        }

        public IContextConfiguration Configuration => _configuration;
        public IRepository<T> RepositoryFor<T>() where T : Model
        {
            return _repoSet[typeof(T)] as IRepository<T>;
        }
    }
}
