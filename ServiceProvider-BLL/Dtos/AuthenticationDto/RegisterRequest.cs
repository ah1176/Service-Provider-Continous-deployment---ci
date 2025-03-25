using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceProvider_BLL.Dtos.AuthenticationDto
{
    public record RegisterationRequest(

        string Email,
        string Password,
        string FullName,
        string BusinessName,
        string BusinessType,
        string TaxNumber,
        List<int> SubCategoryIds
    );

}
