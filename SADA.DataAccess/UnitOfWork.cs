using Microsoft.EntityFrameworkCore;
using SADA.Core.Interfaces;
using SADA.Core.Interfaces.Repositories;
using SADA.Core.Models;
using SADA.DataAccess.Repositories;

namespace SADA.DataAccess
{
    public class UnitOfWork : IUnitOfWork
    {
        //The only place can access database 
        private readonly ApplicationDbContext _db;
       
        public UnitOfWork(ApplicationDbContext db)
        {
            _db = db;

            //Initialize App Repositories
            Category = new BaseRepository<Category>(_db.Set<Category>());
            Product  = new ProductRepository(_db.Set<Product>());
            ShoppingCart = new ShoppingCartRepository(_db.Set<ShoppingCart>());
            ApplicationUser = new BaseRepository<ApplicationUser>(_db.Set<ApplicationUser>());
            OrderHeader = new OrderHeaderRepository(_db.Set<OrderHeader>());
            OrderDetail = new BaseRepository<OrderDetail>(_db.Set<OrderDetail>());
        }

        public IBaseRepository<Category> Category { get; private set; }
        public IProductRepository Product { get; private set; }
        public IShoppingCartRepository ShoppingCart { get; private set; }
        public IBaseRepository<ApplicationUser> ApplicationUser { get; private set; }
        public IOrderHeaderRepository OrderHeader { get; private set; }
        public IBaseRepository<OrderDetail> OrderDetail { get; private set; }


        public void Save() => _db.SaveChanges();
    }
}