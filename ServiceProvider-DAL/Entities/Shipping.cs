using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceProvider_DAL.Entities
{
    public class Shipping
    {
        public int Id { get; set; }

        public DateTime EstimatedDeliveryDate { get; set; }

        public string Status { get; set; } = string.Empty;

        public int OrderId { get; set; }

        public virtual Order Order { get; set; }
    }
}
