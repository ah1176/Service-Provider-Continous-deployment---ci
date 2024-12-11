using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceProvider_DAL.Entities
{
    public class VendorSubCategory
    {
        public int Id { get; set; }
        public string VendorId { get; set; } = string.Empty;
        public int SubCategoryId { get; set; }

        public virtual Vendor Vendor { get; set; } = default!;

        public virtual SubCategory SubCategory { get; set; } = default!;
    }
}
