using SADA.Core.Models;
using SADA.Service;
using System.Linq.Expressions;

namespace SADA.Core.Interfaces.Repositories
{
    public interface IShoppingCartRepository : IBaseRepository<ShoppingCart>
    {
        public int IncrementCount(ShoppingCart shoppingCart, int count);
        public int DecrementCount(ShoppingCart shoppingCart, int count);

    }
}
