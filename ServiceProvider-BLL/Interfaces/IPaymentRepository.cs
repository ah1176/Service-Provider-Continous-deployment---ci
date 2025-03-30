﻿using SeeviceProvider_BLL.Abstractions;
using ServiceProvider_BLL.Abstractions;
using ServiceProvider_BLL.Dtos.Common;
using ServiceProvider_BLL.Dtos.PaymentDto;
using ServiceProvider_DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceProvider_BLL.Interfaces
{
    public interface IPaymentRepository : IBaseRepository<Payment>
    {
        Task<Result<PaginatedList<TransactionResponse>>> GetAllTransactions(RequestFilter request, CancellationToken cancellationToken = default);
        Task<Result<PaginatedList<TransactionResponse>>> GetUserTransactions(string userId, RequestFilter request, CancellationToken cancellationToken = default);
    }
}
