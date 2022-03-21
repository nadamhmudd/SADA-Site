global using SADA.Core.Entities;

namespace SADA.Core.ViewModels;
public class OrderManagementVM
{
    public OrderHeader OrderHeader { get; set; }
    public IEnumerable<OrderDetail> OrderDetails { get; set; }
}
