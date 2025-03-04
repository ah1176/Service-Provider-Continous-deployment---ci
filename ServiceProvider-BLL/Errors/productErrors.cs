using Microsoft.AspNetCore.Http;
using SeeviceProvider_BLL.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceProvider_BLL.Errors
{
    public static class ProductErrors
    {
        public static readonly Error NotFound = new("Not Found", "No menu items found for this provider", StatusCodes.Status404NotFound);
        public static readonly Error ProductNotFound = new("Not Found", "Product does not exist", StatusCodes.Status404NotFound);
        public static readonly Error ProductsNotFound = new("Not Found", " No products Found", StatusCodes.Status404NotFound);
    }
}
