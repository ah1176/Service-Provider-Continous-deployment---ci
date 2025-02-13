using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using ServiceProvider_BLL.Abstractions;
using ServiceProvider_BLL.Interfaces;
using ServiceProvider_DAL.Entities;

namespace SeeviceProvider_PL.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriesController(IUnitOfWork categoryRepositry) : ControllerBase
    {
        private readonly IUnitOfWork _categoryRepositry = categoryRepositry;

        [HttpGet("")]
        public async Task<IActionResult> GetAllCategories(CancellationToken cancellationToken) 
        {
            var result = await _categoryRepositry.Categories.GetCategoriesAsync(cancellationToken);

            return result.IsSuccess ?
            Ok(result.Value)
            :result.ToProblem();

        }

        [HttpGet("{categoryId}/providers")]
        public async Task<IActionResult> GetProvidersByCategory([FromRoute] int categoryId , CancellationToken cancellationToken)
        {
            var result = await _categoryRepositry.Categories.GetProvidersByCategoryAsync(categoryId , cancellationToken);
            return result.IsSuccess
                ? Ok(result.Value)
                : result.ToProblem();
        }
    }
}
