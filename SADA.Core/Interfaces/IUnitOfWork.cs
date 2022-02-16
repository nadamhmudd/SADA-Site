using SADA.Core.Interfaces.Repositories;
using SADA.Core.Models;

namespace SADA.Core.Interfaces
{
    public interface IUnitOfWork
    {
        //Register App Repositories
        public IBaseRepository<Category> Category { get; }
        public IProductRepository Product { get; }
        public IShoppingCartRepository ShoppingCart { get; }
        public IBaseRepository<ApplicationUser> ApplicationUser { get; }
        public IOrderHeaderRepository OrderHeader { get; }
        public IBaseRepository<OrderDetail> OrderDetail { get; }

        //Global Methods
        public void Save();
    }
}
