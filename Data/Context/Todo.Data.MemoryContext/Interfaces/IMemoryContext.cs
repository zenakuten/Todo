using System;
using System.Collections.Generic;
using System.Text;

namespace Todo.Data.Context.MemoryContext.Interfaces
{
    using Todo.Data.Interfaces;
    using Todo.Domain.Models;
    public interface IMemoryContext : IContext
    {
        /// <summary>
        /// Returns the store for the specified model type
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        List<T> StoreFor<T>() where T : Model;

        int NextId<T>() where T : Model;

        /// <summary>
        /// Load store from disk
        /// </summary>
        void Load();

        /// <summary>
        /// Save store to disk
        /// </summary>
        void Save();
    }
}
