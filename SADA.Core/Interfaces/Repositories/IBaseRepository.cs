using SADA.Core.Models;
using SADA.Service;
using System.Linq.Expressions;

namespace SADA.Core.Interfaces.Repositories
{
    //Generic Class
    public interface IBaseRepository<T> where T : class
    {

        //CRUD Operations
        public void Add(T entity);
        public void AddRange(IEnumerable<T> entities);
        public IEnumerable<T> GetAll(Expression<Func<T, object>>? orderBy = null, string orderByDirection = SD.Ascending);
        public void Update(T entity);
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
