using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceProvider_BLL.Dtos.AnalyticsDto.cs
{
    public record OverAllStatisticsResponse(
       int TotalUsersCount,
       int TotalVendorsCount,
       decimal TotalRevenue
    );
}
