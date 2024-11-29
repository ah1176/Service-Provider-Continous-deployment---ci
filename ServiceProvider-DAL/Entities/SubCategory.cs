using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceProvider_DAL.Entities
{
    public class SubCategory
    {
        public int Id { get; set; }

        public string Name { get; set; } = string.Empty;

        public int CategoryId { get; set; }

        public virtual Category Category { get; set; } = default!;

        public virtual ICollection<Product> Products { get; set; } = new List<Product>();
        public virtual ICollection<VendorSubCategory> VendorSubCategories { get; set; } = new List<VendorSubCategory>();

    }
}
