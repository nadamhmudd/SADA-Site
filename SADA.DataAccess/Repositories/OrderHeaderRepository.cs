using Microsoft.EntityFrameworkCore;
using SADA.Core.Interfaces.Repositories;
using SADA.Core.Models;

namespace SADA.DataAccess.Repositories
{
    public class OrderHeaderRepository : BaseRepository<OrderHeader>, IOrderHeaderRepository
    {
        public OrderHeaderRepository(DbSet<OrderHeader> dbSet) : base(dbSet)
        {
        }

        public void UpdateStatus(int id, string orderStatus, string? paymentStatus = null)
        {
            var obj = _dbSet.Find(id);
            if(obj != null)
            {
                obj.OrderStatus = orderStatus;
                if(paymentStatus != null)
                {
                    obj.PaymentStatus = paymentStatus;
                }
            }
        }
    }
}
