using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using SADA.Core.Entities;

namespace SADA.Infrastructure.EF;

public class ApplicationDbContext : IdentityDbContext
{
    //General setup that will confiqure DB Context
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
    {
    }

    //Create Tables
    public DbSet<Governorate> Governorates { get; set; }
    public DbSet<City> Cities { get; set; }
    public DbSet<PaymentMethod> PaymentMethods { get; set; }
    public DbSet<Category> Categories { get; set; }
    public DbSet<Product> Products { get; set; }
    public DbSet<ApplicationUser> ApplicationUsers { get; set; }
    public DbSet<ShoppingCart> ShoppingCarts { get; set; }
    public DbSet<OrderHeader> OrderHeader { get; set; }
}