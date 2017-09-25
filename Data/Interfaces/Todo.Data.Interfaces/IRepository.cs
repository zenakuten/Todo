using System;

namespace Todo.Data.Interfaces
{
    using System.Collections.Generic;
    using Todo.Domain.Models;
    public interface IRepository<T> where T : Model
    {
    }
}
