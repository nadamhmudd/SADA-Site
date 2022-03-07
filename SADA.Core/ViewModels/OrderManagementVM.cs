using SADA.Core.Models;

namespace SADA.Core.ViewModels;
public class OrderManagementVM
{
    public OrderHeader OrderHeader { get; set; }
    public IEnumerable<OrderDetail> OrderDetails { get; set; }
}
