using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceProvider_DAL.Entities
{
    public class CartProduct
    {

        public int Quantity { get; set; }

        public int CartId { get; set; }

        public virtual Cart Cart { get; set; } = default!;

        public int ProductId { get; set; }

        public virtual Product Product { get; set; } = default!;
    }
}
