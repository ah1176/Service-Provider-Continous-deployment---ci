using Microsoft.AspNetCore.Http;
using SeeviceProvider_BLL.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceProvider_BLL.Errors
{
    public static class SubCategoryErrors
    {
        public static readonly Error SubCategoryNotFound = new("Not Found", "SubCategory does not exist", StatusCodes.Status404NotFound);
    }
}
