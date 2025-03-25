using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceProvider_BLL.Dtos.VendorDto
{
    public record RegisterVendorResponse
    (    string UserName,
         string Email,
         string BusinessName,
         string BusinessType,
         [DataType(DataType.Password)] string Password,
         float Rating
    );
}
