using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ServiceProvider_BLL.Dtos.CartProductDto;
using ServiceProvider_BLL.Interfaces;
using ServiceProvider_DAL.Entities;

namespace SeeviceProvider_PL.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CartsController(IUnitOfWork CartProductRepositry) : ControllerBase
    {
        private readonly IUnitOfWork _cartProductRepositry = CartProductRepositry;


        [HttpPost("add")]
        public async Task<IActionResult> AddToCart(CartProductRequest request ,CancellationToken cancellationToken)
        {
            var result = await _cartProductRepositry.CartProducts.AddItemToCartAsync(request, cancellationToken);
            return result.IsSuccess ?
                CreatedAtAction(nameof(AddToCart), new { cartId = request.CartId, productId = request.ProductId }, request)
                : Problem(statusCode: StatusCodes.Status404NotFound, title: result.Error.code, detail: result.Error.description);
        }
    }
}
