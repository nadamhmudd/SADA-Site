namespace SADA.Web.Areas.APIs.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderManagementController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;

        public OrderManagementController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        [HttpGet]
        public IActionResult GetAll(string status)
        {
            IEnumerable<OrderHeader>? orderslist = null;
            if (String.IsNullOrWhiteSpace(status) || status == "all")
            {
                orderslist = _unitOfWork.OrderHeader.GetAll("ApplicationUser", o => o.Id, SD.Descending);
            }
            else
            {
                orderslist = _unitOfWork.OrderHeader.GetAll("ApplicationUser", o => o.Id, SD.Descending,
                    criteria: o => o.OrderStatus == status
                    );
            }

            if (orderslist == null)
                return NotFound();

            return Ok(orderslist);
        }
    }
}