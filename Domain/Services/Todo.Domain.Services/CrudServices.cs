using System;

namespace Todo.Domain.Services
{
    using Todo.Domain.Interfaces;
    using Todo.Domain.Models;
    using Todo.Data.Interfaces;
    public class CrudServices<T> : ServiceBase<T>, ICrudServices<T> where T : Model
    {
        public CrudServices(ICrudRepository<T> repo) : base(repo)
        {
        }

        private ICrudRepository<T> CrudRepository => Repository as ICrudRepository<T>;

        virtual public T Create(T model)
        {
            return CrudRepository.Create(model);
        }

        virtual public bool Delete(T model)
        {
            return CrudRepository.Delete(model);
        }

        virtual public bool Delete(int Id)
        {
            return CrudRepository.Delete(CrudRepository.Read(Id));
        }

        virtual public T Read(int Id)
        {
            return CrudRepository.Read(Id);
        }

        virtual public T Update(T model)
        {
            return CrudRepository.Update(model);
        }

        virtual public T Save(T model)
        {
            return CrudRepository.Read(model.Id) == null ? CrudRepository.Create(model) : CrudRepository.Update(model);
        }
    }
}
