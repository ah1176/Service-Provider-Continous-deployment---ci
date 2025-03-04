using ServiceProvider_BLL.Dtos.OrderProductDto;
using ServiceProvider_BLL.Dtos.PaymentDto;
using ServiceProvider_BLL.Dtos.ShippingDto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceProvider_BLL.Dtos.OrderDto
{
    public record OrderResponseV2(
        int Id,
        decimal TotalAmount,
        DateTime OrderDate,
        string Status,
        List<OrderProductResponse> Products,
        PaymentResponse Payment,
        ShippingResponse? Shipping
    );
 

    
}
