using Microsoft.EntityFrameworkCore;
using SADA.Core.Entities;
using SADA.Core.Interfaces.Repositories;

namespace SADA.Infrastructure.Repositories;
public class OrderHeaderRepository : BaseRepository<OrderHeader>, IOrderHeaderRepository
{
    public OrderHeaderRepository(DbSet<OrderHeader> dbSet) : base(dbSet)
    {
    }

    public void UpdateStatus(int id, string orderStatus, string paymentStatus = null)
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
    public void UpdateStripePaymentID(int id, string sessionId, string paymentIntentId)
    {
        var obj = _dbSet.Find(id);
        obj.PaymentDate = DateTime.Now;
        obj.SessionId = sessionId;
        obj.PaymentIntentId = paymentIntentId;
    }
}
