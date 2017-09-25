using System;
using System.Collections.Generic;
using System.Text;

namespace Todo.Data.Context
{
    using Todo.Domain.Models;
    using Todo.Data.Interfaces;

    /// <summary>
    /// Abstract base class for all repositories
    /// 
    /// context - repository context
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public abstract class Repository<T> : IRepository<T> where T : Model
    {
        protected IContext _context;
        public Repository(IContext context)
        {
            _context = context;
        }

    }
}
