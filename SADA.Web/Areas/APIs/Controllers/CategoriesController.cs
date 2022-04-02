namespace SADA.Web.Areas.APIs.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriesController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;

        public CategoriesController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            var list = _unitOfWork.Category.GetAll(null, o => o.DisplayOrder);

            if (list is null)
                return NotFound();

            return Ok(list);
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var obj = _unitOfWork.Category.GetFirstOrDefault(p => p.Id == id);

            if (obj == null)
                return NotFound();

            _unitOfWork.Category.Remove(obj);

            _unitOfWork.Save();

            return Ok(obj);
        }
    }
}