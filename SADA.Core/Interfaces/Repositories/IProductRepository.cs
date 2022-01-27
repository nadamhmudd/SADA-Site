using SADA.Core.Models;
using SADA.Service;
using System.Linq.Expressions;

namespace SADA.Core.Interfaces.Repositories
{
    public interface IProductRepository : IBaseRepository<Product>
    {
        public void Update(Product entity);

    }
}
