using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceProvider_DAL.Entities
{
    public class Product
    {
        public int ProductId { get; set; }
        public string Name { get; set; } 
        public string? Description { get; set;} 
        public decimal Price { get; set; } 
        public string ImageUrl {  get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public int SubCategoryId { get; set; }
        public string VendorId { get; set; }

        public virtual SubCategory SubCategory { get; set; }
        public virtual Vendor Vendor { get; set; }
        public virtual ICollection<OrderProduct>? OrderProducts { get; set; } = new List<OrderProduct>();
        public virtual ICollection<Review>? Reviews { get; set;} = new List<Review>();
        public virtual ICollection<CartProduct>? CartProducts { get; set; } = new List<CartProduct>();
    }
}
