using SADA.Core.Interfaces.Repositories;
using SADA.Core.Models;

namespace SADA.Core.Interfaces
{
    public interface IUnitOfWork
    {
        //Register App Repositories
        public IBaseRepository<Category> Category { get; }
        public IProductRepository Product { get; }
        public IBaseRepository<ShoppingCart> ShoppingCart { get; }
        public IBaseRepository<ApplicationUser> ApplicationUser { get; }


        //Global Methods
        public void Save();
    }
}
