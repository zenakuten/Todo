using System;
using System.Collections.Generic;
using System.Text;

namespace Todo.Data.Interfaces
{
    using Todo.Domain.Models;
    public interface ICrudRepository<T> : IRepository<T> where T : Model
    {
        /// <summary>
        /// Creates the model, returning the created entity
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        T Create(T model);

        /// <summary>
        /// Reads the model
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        T Read(int Id);

        /// <summary>
        /// Updates the model, returning the updated model from the store
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        T Update(T model);

        /// <summary>
        /// Deletes the model, returning false if there was an error
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        bool Delete(T model);

    }
}
