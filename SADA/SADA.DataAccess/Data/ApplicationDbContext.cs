using Microsoft.EntityFrameworkCore;
using SADA.Models;

namespace SADA.Database;
public class ApplicationDbContext : DbContext
{
    //General setup that will confiqure DB Context
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
    {
    }

    //Create Tables
    public DbSet<Category> Categories { get; set; }
}

