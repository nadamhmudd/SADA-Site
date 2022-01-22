using Microsoft.EntityFrameworkCore;
using SADA.Core.Interfaces.Repositories;
using SADA.Service;
using System.Linq.Expressions;

namespace SADA.DataAccess.Repositories
{
    public class BaseRepository<T> : IBaseRepository<T> where T : class
    {
        protected DbSet<T> _dbSet;
        public BaseRepository(DbSet<T> dbSet) => _dbSet = dbSet;

        //CRUD opertaions

        public void Add(T entity) => _dbSet.Add(entity);

        public void AddRange(IEnumerable<T> entities) => _dbSet.AddRange(entities);
        
        public IEnumerable<T> GetAll() => _dbSet;

        public void Update(T entity) => _dbSet.Update(entity);

        public void Remove(T entity) => _dbSet.Remove(entity);

        public void RemoveRange(IEnumerable<T> entities) => _dbSet.RemoveRange(entities);


        //Search operations
        public T GetById(int id) => _dbSet.Find(id);

        public T GetFirstOrDefault(Expression<Func<T, bool>> criteria) => _dbSet.FirstOrDefault(criteria);

        public IEnumerable<T> FindAll(Expression<Func<T, bool>> criteria,
                                      Expression<Func<T, object>>? orderBy = null,
                                      string orderByDirection = SD.Ascending)
        {
            IQueryable<T> query = _dbSet.Where(criteria);
            if (orderBy != null)
                if (orderByDirection == SD.Ascending)
                    query = query.OrderBy(orderBy);
                else
                    query = query.OrderByDescending(orderBy);

            return query.ToList();

        }

        //Aggregating operations
        public int Count() => _dbSet.Count();

    }
}
