using System.Collections.Generic;
using System.Linq;
using MvcSolution;
using MvcSolution.Data.Context;
using MvcSolution.Data.Entities;

namespace MVCSolution.Services
{
    public abstract class ServiceBase<T> : IServiceBase<T> where T : BusinessBase
    {
        protected MvcSolutionDataContext NewDB()
        {
            return new MvcSolutionDataContext();
        }

        #region Implementation of IServiceBase<T>

        public T Get(int id)
        {
            using(var db = NewDB())
            {
                return db.Set<T>().Get(id);
            }
        }

        public List<T> GetAll()
        {
            using(var db = NewDB())
            {
                return db.Set<T>().ToList();
            }
        }

        public void Add(T entity)
        {
            using(var db = NewDB())
            {
                db.Set<T>().Add(entity);
                db.SaveChanges();
            }
        }

        #endregion
    }
}
