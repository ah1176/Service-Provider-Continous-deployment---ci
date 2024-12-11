using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
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
        public async Task<IActionResult> GetAllCategories() 
        {
            var result = await _categoryRepositry.Categories.GetAllAsync();

            return result.IsSuccess ?
            Ok(result.Value)
            : Problem(statusCode: StatusCodes.Status404NotFound, title: result.Error.code, detail: result.Error.description);

        }
    }
}
