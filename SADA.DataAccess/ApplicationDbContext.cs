using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using SADA.Core.Models;

namespace SADA.DataAccess
{
    public class ApplicationDbContext : IdentityDbContext
    {
        //General setup that will confiqure DB Context
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        //Create Tables
        public DbSet<Category> Categories { get; set; }
        public DbSet<Product> Products { get; set; }

    }
}