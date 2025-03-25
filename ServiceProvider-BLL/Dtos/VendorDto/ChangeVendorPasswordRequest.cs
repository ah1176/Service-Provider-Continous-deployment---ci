using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceProvider_BLL.Dtos.VendorDto
{
    public record ChangeVendorPasswordRequest(
        string CurrentPassword,
        string NewPassword
    );
    
}
