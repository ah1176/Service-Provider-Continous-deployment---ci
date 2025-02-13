using Mapster;
using Microsoft.EntityFrameworkCore;
using SeeviceProvider_BLL.Abstractions;
using ServiceProvider_BLL.Dtos.OrderDto;
using ServiceProvider_BLL.Errors;
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
    public class OrderRepository : BaseRepository<Order> , IOrderRepository
    {
        private readonly AppDbContext _context;

        public OrderRepository(AppDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<Result<OrderResponse>> CheckoutAsync(CheckoutRequest request, CancellationToken cancellationToken)
        {
            await using var transaction = await _context.Database.BeginTransactionAsync();

            try
            {
                var cart = await _context.Carts!
                    .Include(c => c.CartProducts)
                    .ThenInclude(cp => cp.Product)
                    .FirstOrDefaultAsync(c => c.ApplicationUserId == request.UserId, cancellationToken: cancellationToken);

                if (cart == null || !cart.CartProducts.Any())
                    return Result.Failure<OrderResponse>(CartErrors.CartNotFoundOrEmpty);

                var order = new Order
                {
                    ApplicationUserId = request.UserId,
                    TotalAmount = cart.CartProducts.Sum(cp => cp.Quantity * cp.Product.Price),
                    OrderDate = DateTime.UtcNow,
                    Status = OrderStatus.Pending,
                };

                await _context.Orders!.AddAsync(order, cancellationToken);
                await _context.SaveChangesAsync(cancellationToken);

                var payment = new Payment
                {
                    OrderId = order.Id,
                    TotalAmount = order.TotalAmount,
                    Status = PaymentStatus.Completed,
                    PaymentMethod = "Credit Card"
                };

               await _context.Payments!.AddAsync(payment, cancellationToken);
                _context.CartProducts!.RemoveRange(cart.CartProducts);
                await _context.SaveChangesAsync(cancellationToken);

                await transaction.CommitAsync(cancellationToken);
                return Result.Success(order.Adapt<OrderResponse>());
            }
            catch
            {
                await transaction.RollbackAsync(cancellationToken);
                return Result.Failure<OrderResponse>(CheckOutErrors.CheckOutFaild);
            }
        }
    }
}
