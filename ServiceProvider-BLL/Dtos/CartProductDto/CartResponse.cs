using ServiceProvider_BLL.Dtos.ProductDto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceProvider_BLL.Dtos.CartProductDto
{
    public record CartResponse(
        int Id,
        IEnumerable<CartItemResponse> Items
        );

}
