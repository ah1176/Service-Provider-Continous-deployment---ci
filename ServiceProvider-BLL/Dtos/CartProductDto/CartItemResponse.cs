using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceProvider_BLL.Dtos.CartProductDto
{
    public record CartItemResponse(
        int ProductId,
        string NameEn,
        string NameAr,
        decimal Price,
        int Quantity
        );
}
