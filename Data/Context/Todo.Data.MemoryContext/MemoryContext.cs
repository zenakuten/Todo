using System;
using System.Collections.Generic;
namespace Todo.Data.Context.MemoryContext
{
    using Todo.Data.Interfaces;
    using Todo.Domain.Models;
    using Todo.Data.Context;
    using Todo.Data.Context.MemoryContext.Interfaces;
    using Todo.Data.Context.MemoryContext.Repositories;
    using Newtonsoft.Json;
    using System.Linq;
    using System.Linq.Expressions;

    /// <summary>
    /// Represents and in memory data context
    /// 
    /// Configuration:
    /// 
    /// DataFile: Filename to use for storage and retrieval
    ///
    ///     
    /// </summary>
    public class MemoryContext : Context, IMemoryContext
    {
        private IUserRepository _userRepository;
        public override IUserRepository Users => _userRepository;

        private IListRepository _listRepository;
        public override IListRepository Lists => _listRepository;

        private IListItemRepository _listItemRepository;
        public override IListItemRepository ListItems => _listItemRepository;

        private const string DataFile = "DataFile";

        private Dictionary<Type, object> _storeSet { get; set; }
        private List<UserModel> UserStore => StoreFor<UserModel>();
        private List<ListModel> ListStore => StoreFor<ListModel>();
        private List<ListItemModel> ListItemStore => StoreFor<ListItemModel>();

        private Dictionary<Type, int> _nextIds { get; set; }

        public MemoryContext(IContextConfiguration configuration) : base(configuration)
        {
            _storeSet = new Dictionary<Type, object>();
            _storeSet.Add(typeof(UserModel), new List<UserModel>());
            _storeSet.Add(typeof(ListModel), new List<ListModel>());
            _storeSet.Add(typeof(ListItemModel), new List<ListItemModel>());

            _userRepository = new UserRepository(this);
            _listRepository = new ListRepository(this);
            _listItemRepository = new ListItemRepository(this);

            _nextIds = new Dictionary<Type, int>();
            _nextIds.Add(typeof(UserModel), 1);
            _nextIds.Add(typeof(ListModel), 1);
            _nextIds.Add(typeof(ListItemModel), 1);

        }

        public List<T> StoreFor<T>() where T : Model
        {
            return _storeSet[typeof(T)] as List<T>;
        }

        public int NextId<T>() where T : Model
        {
            return _nextIds[typeof(T)]++;
        }

        public class DataStore
        {
            public List<UserModel> Users { get; set; }
            public List<ListModel> Lists { get; set; }
            public List<ListItemModel> ListItems { get; set; }
        }

        public void Load()
        {
            var file = Configuration.Settings[DataFile];
            var inStream = new System.IO.StreamReader(file);
            var jsonString = inStream.ReadToEnd();
            inStream.Close();

            var dataStore = JsonConvert.DeserializeObject<DataStore>(jsonString);

            _storeSet[typeof(UserModel)] = dataStore.Users;
            _storeSet[typeof(ListModel)] = dataStore.Lists;
            _storeSet[typeof(ListItemModel)] = dataStore.ListItems;

            _nextIds[typeof(UserModel)] = dataStore.Users.Select(m => m.Id).Prepend(0).Max() + 1;
            _nextIds[typeof(ListModel)] = dataStore.Lists.Select(m => m.Id).Prepend(0).Max() + 1;
            _nextIds[typeof(ListItemModel)] =  dataStore.ListItems.Select(m => m.Id).Prepend(0).Max() + 1;
        }

        public void Save()
        {
            var file = Configuration.Settings[DataFile];
            var dataStore = new DataStore();
            dataStore.Users = UserStore;
            dataStore.Lists = ListStore;
            dataStore.ListItems = ListItemStore;

            var jsonString = JsonConvert.SerializeObject(dataStore);
            var outStream = new System.IO.StreamWriter(file);
            outStream.Write(jsonString);
            outStream.Close();
        }        
    }
}
