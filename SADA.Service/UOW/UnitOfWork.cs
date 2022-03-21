using SADA.Core.Interfaces;
using SADA.Core.Entities;
using SADA.Infrastructure.Repositories;
using SADA.Infrastructure.EF;
using SADA.Core.Interfaces.Repositories;

namespace SADA.Service
{
    public class UnitOfWork : IUnitOfWork
    {
        //The only place can access database 
        private readonly ApplicationDbContext _db;
       
        public UnitOfWork(ApplicationDbContext db)
        {
            _db = db;

            //Initialize App Repositories
            Governorate = new BaseRepository<Governorate>(_db.Set<Governorate>());
            City = new BaseRepository<City>(_db.Set<City>());
            PaymentMethod = new BaseRepository<PaymentMethod>(_db.Set<PaymentMethod>());

            ApplicationUser = new BaseRepository<ApplicationUser>(_db.Set<ApplicationUser>());

            Category = new BaseRepository<Category>(_db.Set<Category>());
            Product  = new ProductRepository(_db.Set<Product>());
            ProductImages = new BaseRepository<ProductImage>(_db.Set<ProductImage>());
            ProductSizes = new BaseRepository<ProductSize>(_db.Set<ProductSize>());
            ProductColors = new BaseRepository<ProductColor>(_db.Set<ProductColor>());

            ShoppingCart = new ShoppingCartRepository(_db.Set<ShoppingCart>());
            OrderHeader = new OrderHeaderRepository(_db.Set<OrderHeader>());
            OrderDetail = new BaseRepository<OrderDetail>(_db.Set<OrderDetail>());
        }

        public IBaseRepository<Governorate> Governorate { get; private set; }
        public IBaseRepository<City> City { get; private set; }
        public IBaseRepository<PaymentMethod> PaymentMethod { get; private set; }

        public IBaseRepository<ApplicationUser> ApplicationUser { get; private set; }

        public IBaseRepository<Category> Category { get; private set; }
        public IProductRepository Product { get; private set; }
        public IBaseRepository<ProductImage> ProductImages { get; private set; }
        public IBaseRepository<ProductSize> ProductSizes { get; private set; }
        public IBaseRepository<ProductColor> ProductColors { get; private set; }

        public IShoppingCartRepository ShoppingCart { get; private set; }
        public IOrderHeaderRepository OrderHeader { get; private set; }
        public IBaseRepository<OrderDetail> OrderDetail { get; private set; }

        public void Save() => _db.SaveChanges();
    }
}