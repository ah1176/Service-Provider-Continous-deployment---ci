using Microsoft.AspNetCore.Http;
using SeeviceProvider_BLL.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceProvider_BLL.Errors
{
    public static class UserErrors
    {
        public static readonly Error NotFound = new("Not Found", "no users found", StatusCodes.Status404NotFound);
    }
}
