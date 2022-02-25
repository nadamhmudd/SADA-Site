using Microsoft.EntityFrameworkCore;
using SADA.Core.Interfaces.Repositories;
using SADA.Core.Models;

namespace SADA.DataAccess.Repositories
{
    public class ProductRepository : BaseRepository<Product>, IProductRepository
    {
        public ProductRepository(DbSet<Product> dbSet) : base(dbSet)
        {
        }

        //override
        public void Update(Product entity)
        {
            if (entity.CoverUrl != null) //image updated
            {
                _dbSet.Update(entity);
            }
            else
            {
                var objFromData = _dbSet.FirstOrDefault(u => u.Id == entity.Id);
                if (objFromData != null)
                {
                    objFromData.Name = entity.Name;
                    objFromData.Description = entity.Description;
                    objFromData.Price = entity.Price;
                    objFromData.DiscountAmount = entity.DiscountAmount;
                    objFromData.DiscountPercentage = entity.DiscountPercentage;
                    objFromData.CategoryId = entity.CategoryId;
                }
            }
        }
    }
}
