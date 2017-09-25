using System;
using System.Collections.Generic;
using System.Text;

namespace Todo.Domain.Services
{
    using Todo.Domain.Models;
    using Todo.Data.Interfaces;

    public abstract class ServiceBase<T> where T : Model
    {
        protected IRepository<T> Repository;
        public ServiceBase(IRepository<T> repository)
        {
            Repository = repository;
        }
    }
}
