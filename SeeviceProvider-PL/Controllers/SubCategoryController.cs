using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ServiceProvider_BLL.Abstractions;
using ServiceProvider_BLL.Interfaces;
using ServiceProvider_BLL.Reposatories;

namespace SeeviceProvider_PL.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SubCategoryController(IUnitOfWork SubCategoryRepositry) : ControllerBase
    {
        private readonly IUnitOfWork _subcategoryRepositry = SubCategoryRepositry;

        [HttpGet("")]
        public async Task<IActionResult> GetAllSubCategories(CancellationToken cancellationToken)
        {
            var result = await _subcategoryRepositry.SubCategories.GetSubCategoriesAsync(cancellationToken);

            return result.IsSuccess ?
            Ok(result.Value)
            : result.ToProblem();

        }
    }
}
