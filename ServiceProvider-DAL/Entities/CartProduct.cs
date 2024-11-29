using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceProvider_DAL.Entities
{
    public class CartProduct
    {
        public int Id { get; set; }

        public int Quantity { get; set; }

        public int CartId { get; set; }

        public int ProductId { get; set; }

        public virtual Cart Cart { get; set; } = default!;

        public virtual Product Product { get; set; } = default!;
    }
}
