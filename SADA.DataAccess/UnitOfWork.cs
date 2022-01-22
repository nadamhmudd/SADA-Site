using SADA.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SADA.DataAccess
{
    public class UnitOfWork : IUnitOfWork
    {
        //the only place that connect with database context
        private readonly ApplicationDbContext _db;
        public UnitOfWork(ApplicationDbContext db)
        {
            _db = db;
        }

        public void Save() => _db.SaveChanges();
    }
}