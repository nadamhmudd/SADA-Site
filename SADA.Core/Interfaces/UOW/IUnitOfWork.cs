using SADA.Core.Interfaces.Repositories;

namespace SADA.Core.Interfaces;
public interface IUnitOfWork
{
    //Register App Repositories
    public IBaseRepository<Governorate> Governorate { get; }
    public IBaseRepository<City> City { get; }
    public IBaseRepository<PaymentMethod> PaymentMethod { get; }

    public IBaseRepository<ApplicationUser> ApplicationUser { get; }
    public IShoppingCartRepository ShoppingCart { get; }

    public IBaseRepository<Category> Category { get; }
    public IProductRepository Product { get; }

    public IOrderHeaderRepository OrderHeader { get; }

    //Global Methods
    public void Save();
}
