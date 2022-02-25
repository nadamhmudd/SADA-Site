using Microsoft.EntityFrameworkCore;
using SADA.Core.Interfaces.Repositories;
using SADA.Core.Models;

namespace SADA.DataAccess.Repositories
{
    public class ShoppingCartRepository : BaseRepository<ShoppingCart>, IShoppingCartRepository
    {
        public ShoppingCartRepository(DbSet<ShoppingCart> dbSet) : base(dbSet)
        {
        }

        public void IncrementCount(ShoppingCart shoppingCart, int count)
        {
            shoppingCart.Count += count;
        }
        public void DecrementCount(ShoppingCart shoppingCart, int count)
        {
                shoppingCart.Count -= count;
        }
    }
}
