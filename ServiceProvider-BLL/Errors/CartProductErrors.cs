using Microsoft.AspNetCore.Http;
using SeeviceProvider_BLL.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceProvider_BLL.Errors
{
    public static class CartProductErrors
    {
        public static readonly Error NotFound = new("Not Found", "Cart or Product not found.", StatusCodes.Status404NotFound);
    }
}
