using SeeviceProvider_BLL.Abstractions;
using ServiceProvider_BLL.Dtos.OrderDto;
using ServiceProvider_DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceProvider_BLL.Interfaces
{
    public interface IOrderRepository : IBaseRepository<Order>
    {
        Task<Result<OrderResponse>> CheckoutAsync(CheckoutRequest request , CancellationToken cancellationToken);
    }
}
