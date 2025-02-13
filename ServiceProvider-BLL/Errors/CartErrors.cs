using Microsoft.AspNetCore.Http;
using SeeviceProvider_BLL.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceProvider_BLL.Errors
{
    public static class CartErrors
    {
        public static readonly Error CartNotFoundOrEmpty = new("Not Found", "Cart not found or empty", StatusCodes.Status404NotFound);
    }
}
