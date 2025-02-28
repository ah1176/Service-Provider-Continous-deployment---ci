﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceProvider_BLL.Dtos.PaymentDto
{
    public record PaymentResponse(
       decimal Amount,
       string Status,
       DateTime TransactionDate
    );

}
