using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceProvider_BLL.Dtos.ReviewDto
{
    public record VendorReviewsResponse(
      int Id,
      int Rating,
      string? Comment,
      DateTime? CreatedAt,
      string UserName,
      string ProductNameEn,
      string ProductNameAr
    );
}
