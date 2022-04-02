namespace SADA.Web.Areas.APIs.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IFileHandler _fileHandler;

        public ProductsController(IUnitOfWork unitOfWork, IFileHandler fileHandler)
        {
            _unitOfWork = unitOfWork;
            _fileHandler = fileHandler;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            var productsList = _unitOfWork.Product.GetAll("Category", o => o.Id, SD.Descending);

            if (productsList is null)
                return NotFound();

            return Ok(productsList);
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var obj = _unitOfWork.Product.GetFirstOrDefault(p => p.Id == id);

            if (obj == null)
                return NotFound();

            if (obj.CoverUrl != null) //delete image
                _fileHandler.Image.Delete(obj.CoverUrl);

            _unitOfWork.Product.Remove(obj);

            _unitOfWork.Save();

            return Ok(obj);
        }
    }
}
