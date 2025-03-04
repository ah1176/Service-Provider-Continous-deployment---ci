using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceProvider_BLL.Dtos.OrderDto
{
    public record OrderResponse(
        string ApplicationUserId,
        decimal TotalAmount,
        DateTime OrderDate,
        string Status
    );
    
}
