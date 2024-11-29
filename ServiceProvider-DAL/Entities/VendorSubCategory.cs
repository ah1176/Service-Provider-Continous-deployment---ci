using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceProvider_DAL.Entities
{
    public class VendorSubCategory
    {
        public int VendorId { get; set; }
        public int SubCategoryId { get; set; }

        public virtual Vendor Vendor { get; set; }

        public virtual SubCategory SubCategory { get; set; }
    }
}
