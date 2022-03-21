using SADA.Core.Entities;

namespace SADA.Core.Interfaces.Repositories;
public interface IOrderHeaderRepository : IBaseRepository<OrderHeader>
{
    public void UpdateStatus(int id, string orderStatus, string paymentStatus = null);
    public void UpdateStripePaymentID(int id, string sessionId, string paymentIntentId);
}
