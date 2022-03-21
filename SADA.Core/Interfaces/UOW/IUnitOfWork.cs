using SADA.Core.Interfaces.Repositories;
using SADA.Core.Entities;

namespace SADA.Core.Interfaces;
public interface IUnitOfWork
{
    //Register App Repositories
    public IBaseRepository<Governorate> Governorate { get; }
    public IBaseRepository<City> City { get; }
    public IBaseRepository<PaymentMethod> PaymentMethod { get; }

    public IBaseRepository<ApplicationUser> ApplicationUser { get; }

    public IBaseRepository<Category> Category { get; }
    public IProductRepository Product { get; }
    public IBaseRepository<ProductImage> ProductImages { get; }
    public IBaseRepository<ProductSize> ProductSizes { get; }
    public IBaseRepository<ProductColor> ProductColors { get; }

    public IShoppingCartRepository ShoppingCart { get; }
    public IOrderHeaderRepository OrderHeader { get; }
    public IBaseRepository<OrderDetail> OrderDetail { get; }

    //Global Methods
    public void Save();
}
