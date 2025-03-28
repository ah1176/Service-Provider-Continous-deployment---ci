using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceProvider_BLL.Dtos.AnalyticsDto.cs
{
    public record TodaysStatsResponse(
        int NewUsersCount,
        int OrdersCount,
        decimal RevenueToday,
        int VendorsCount
    );
}
