using Microsoft.AspNetCore.Http;
using SeeviceProvider_BLL.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceProvider_BLL.Errors
{
    public static class CategoryErrors
    {
        public static readonly Error NotFound = new("Not Found", "no categories found", StatusCodes.Status404NotFound);
    }
}
