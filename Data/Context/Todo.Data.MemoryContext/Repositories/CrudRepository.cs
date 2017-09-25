using System;

namespace Todo.Data.Context.MemoryContext.Repositories
{
    using System.Collections.Generic;
    using Todo.Data.Interfaces;
    using Todo.Domain.Models;
    using Todo.Data.Context;
    using Todo.Data.Context.MemoryContext.Interfaces;

    public class CrudRepository<T> : Repository<T>, ICrudRepository<T> where T : Model
    {
        public CrudRepository(IMemoryContext context) : base(context)
        {
        }

        protected List<T> EntitySet => (_context as IMemoryContext).StoreFor<T>();
        protected IMemoryContext _memoryContext => (_context as IMemoryContext);

        public T Create(T model)
        {
            model.Id = _memoryContext.NextId<T>();
            EntitySet.Add(model);
            return Read(model.Id);            
        }

        public bool Delete(T model)
        {
            if (model == null)
                return false;

            var entity = Read(model.Id);
            if (entity == null)
                return false;

            return EntitySet.Remove(entity);
        }

        public T Read(int Id)
        {
            return EntitySet.Find(e => e.Id == Id);
        }

        public T Update(T model)
        {
            int i = EntitySet.FindIndex(e => e.Id == model.Id);
            if (i != -1)
            {
                EntitySet[i] = model;
                return Read(model.Id);
            }

            throw new Exception($"Update: Model {model.Id} does not exist.");
        }
    }
}
