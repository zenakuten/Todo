using System;
using System.Collections.Generic;
using System.Text;

namespace Todo.Domain.Interfaces
{
    using Todo.Domain.Models;
    /// <summary>
    /// Some generic boilerplate for dealing with crud operations
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface ICrudServices<T>  where T : Model
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
        /// <param name="Id">Id of the model to read</param>
        /// <returns>the model</returns>
        T Read(int Id);

        /// <summary>
        /// Updates the model, returning the updated model from the store
        /// </summary>
        /// <param name="model">model to update</param>
        /// <returns>the updated model</returns>
        T Update(T model);

        /// <summary>
        /// Deletes the model, returning false if there was an error
        /// </summary>
        /// <param name="model">model to delete</param>
        /// <returns>true if successful</returns>
        bool Delete(T model);

        /// <summary>
        /// Deletes the model, returning false if there was an error
        /// </summary>
        /// <param name="id">the id of the model</param>
        /// <returns>true if successful</returns>
        bool Delete(int id);

        /// <summary>
        /// Creates or Updates the model
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        T Save(T model);

    }
}
