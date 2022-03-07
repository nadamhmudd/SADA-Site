﻿using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using SADA.Core.Models;

namespace SADA.DataAccess;

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

    public DbSet<ApplicationUser> ApplicationUsers { get; set; }

    public DbSet<Category> Categories { get; set; }
    public DbSet<Product> Products { get; set; }
    public DbSet<ProductImage> ProductImages { get; set; }
    public DbSet<ProductSize> ProductSizes { get; set; }
    public DbSet<ProductColor> ProductColors { get; set; }

    public DbSet<ShoppingCart> ShoppingCarts { get; set; }
    public DbSet<OrderHeader> OrderHeaders { get; set; }
    public DbSet<OrderDetail> OrderDetails { get; set; }
}
