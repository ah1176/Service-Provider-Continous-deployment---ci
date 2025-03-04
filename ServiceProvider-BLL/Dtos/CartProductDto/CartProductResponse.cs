using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceProvider_BLL.Dtos.CartProductDto
{
    public record CartProductResponse(
        int CartId,
        int ProductId,
        int Quantity
    );
    
}
