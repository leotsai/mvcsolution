using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MvcSolution.Data.Entities;

namespace MVCSolution.Services
{
    public interface IServiceBase<T> where T : BusinessBase
    {
        T Get(int id);
        List<T> GetAll();
        void Add(T entity);
    }
}
