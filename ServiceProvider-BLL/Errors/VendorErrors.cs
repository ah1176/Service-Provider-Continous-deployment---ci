using Microsoft.AspNetCore.Http;
using SeeviceProvider_BLL.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceProvider_BLL.Errors
{
    public static class VendorErrors
    {
        public static readonly Error NotFound = new("Not Found", "no Vendors found", StatusCodes.Status404NotFound);
        public static readonly Error IncorrectPassword = new("Vendor.IncorrectPassword", "Incorrect old password.", StatusCodes.Status400BadRequest);
    }
}
