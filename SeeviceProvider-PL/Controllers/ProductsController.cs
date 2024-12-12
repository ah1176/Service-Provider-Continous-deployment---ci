using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ServiceProvider_BLL.Interfaces;

namespace SeeviceProvider_PL.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController(IUnitOfWork productRepositry) : ControllerBase
    {
        private readonly IUnitOfWork _productRepositry = productRepositry;


        [HttpGet("")]
        public async Task<IActionResult> GetVendorsMenu([FromBody] string vendorId , CancellationToken cancellationToken)
        {
            var result = await _productRepositry.Products.GetProductsAsync(vendorId, cancellationToken);

            return result.IsSuccess ? 
                Ok(result.Value) 
                : Problem(statusCode: StatusCodes.Status404NotFound, title: result.Error.code, detail: result.Error.description);
        }
    }
}
