using Microsoft.EntityFrameworkCore;
using SADA.Core.Interfaces.Repositories;
using SADA.Core.Entities;
using SADA.Infrastructure.EF;

namespace SADA.Infrastructure.Repositories
{
    public class ProductRepository : BaseRepository<Product>, IProductRepository
    {
        private readonly ApplicationDbContext _db;
        public ProductRepository(ApplicationDbContext db) : base(db.Set<Product>())
        {
            _db = db;
        }

        //override
        public void Update(Product entity)
        {
            var objFromData = _dbSet.FirstOrDefault(u => u.Id == entity.Id);
            
            if (objFromData != null) { 
             
                if (entity.CoverUrl != null) //image updated
                    objFromData.CoverUrl = entity.CoverUrl;
            
                objFromData.Name = entity.Name;
                objFromData.Description = entity.Description;
                objFromData.StockCount = entity.StockCount;
                objFromData.Price = entity.Price;
                objFromData.OnSale = entity.OnSale;
                objFromData.DiscountAmount = entity.DiscountAmount;
                objFromData.DiscountPercentage = entity.DiscountPercentage;
                objFromData.CategoryId = entity.CategoryId;

                objFromData.Colors.RemoveAll(c => c.Id == c.Id);
                objFromData.Colors = entity.Colors;

                objFromData.Imaages.RemoveAll(c => c.Id == c.Id);
                objFromData.Imaages = entity.Imaages;

                objFromData.Sizes.RemoveAll(c => c.Id == c.Id);
                objFromData.Sizes = entity.Sizes;
            }
        }
    }
}
