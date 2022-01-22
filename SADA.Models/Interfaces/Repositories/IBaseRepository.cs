using SADA.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace SADA.Core.Interfaces.Repositories
{
    //Generic Class
    public interface IBaseRepository<T> where T : class
    {

        //CRUD Operations except update 
        public IEnumerable<T> GetAll(T entity);
        public void Add(T entity);
        public void AddRange(IEnumerable<T> entities);

        public void Remove(T entity);
        public void RemoveRange(IEnumerable<T> entities);

        //Search Operations
        public T GetById(int id);
        public T GetFirstOrDefault(Expression<Func<T, bool>> criteria);
        public IEnumerable<T> FindAll(Expression<Func<T, bool>> criteria,
                                      Expression<Func<T, object>>? orderBy = null,
                                      string orderByDirection = SD.Ascending);

        //Aggregating Operations
        public int Count();
    }
}
