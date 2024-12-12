using Mapster;
using Microsoft.EntityFrameworkCore;
using SeeviceProvider_BLL.Abstractions;
using ServiceProvider_BLL.Dtos.CartProductDto;
using ServiceProvider_BLL.Interfaces;
using ServiceProvider_DAL.Data;
using ServiceProvider_DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceProvider_BLL.Reposatories
{
    public class CartProductRepository : BaseRepository<CartProduct> , ICartProductRepository
    {
        private readonly AppDbContext _context;

        public CartProductRepository(AppDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<Result> AddItemToCartAsync(CartProductRequest request, CancellationToken cancellationToken)
        {
            var cart = await _context.Carts!.FindAsync(request.CartId, cancellationToken);

            var product = await _context.Products!.FindAsync(request.ProductId, cancellationToken);

            if (cart is null || product is null)
                return Result.Failure(new Error("Not Found", "Cart or Product not found."));

            var cartProduct = await _context.CartProducts!
            .FirstOrDefaultAsync(cp => cp.CartId == request.CartId && cp.ProductId == request.ProductId);

            if (cartProduct is null)
                await _context.CartProducts!.AddAsync(request.Adapt<CartProduct>(), cancellationToken);

            else
            {
                cartProduct.Quantity = request.Quantity;

                 _context.Update(cartProduct);
            }

            await _context.SaveChangesAsync(cancellationToken);

            return Result.Success();
        }
    }
}
