using SADA.Core.Entities;

namespace SADA.Core.Interfaces.Repositories;
public interface IShoppingCartRepository : IBaseRepository<ShoppingCart>
{
    public void IncrementCount(ShoppingCart shoppingCart, int count);
    public void DecrementCount(ShoppingCart shoppingCart, int count);
}
