using ServiceProvider_BLL.Dtos.OrderProductDto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceProvider_BLL.Dtos.OrderDto
{
    public record OrdersOfVendorResponse(
      int Id,
      decimal TotalAmount,
      DateTime OrderDate,
      string Status,
      List<OrderProductResponse> OrderProducts
    );
}
