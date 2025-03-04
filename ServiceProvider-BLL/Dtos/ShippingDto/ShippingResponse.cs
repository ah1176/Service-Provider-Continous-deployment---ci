﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceProvider_BLL.Dtos.ShippingDto
{
    public record ShippingResponse(
        string Status,
        DateTime EstimatedDeliveryDate
    );
}
