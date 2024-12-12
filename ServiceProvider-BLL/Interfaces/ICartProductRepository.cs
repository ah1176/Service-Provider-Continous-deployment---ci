using SeeviceProvider_BLL.Abstractions;
using ServiceProvider_BLL.Dtos.CartProductDto;
using ServiceProvider_DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceProvider_BLL.Interfaces
{
    public interface ICartProductRepository : IBaseRepository<CartProduct>
    {
        Task<Result> AddItemToCartAsync(CartProductRequest request ,CancellationToken cancellationToken);
    }
}
