﻿using Microsoft.EntityFrameworkCore;
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
        public IBaseRepository<Category> Category { get; private set; }
        public IProductRepository Product { get; private set; }


        public UnitOfWork(ApplicationDbContext db)
        {
            _db = db;

            //Initialize App Repositories
            Category = new BaseRepository<Category>(_db.Set<Category>());
            Product  = new ProductRepository(_db.Set<Product>());

        }


        public void Save() => _db.SaveChanges();
    }
}