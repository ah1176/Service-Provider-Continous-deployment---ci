using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceProvider_DAL.Entities
{
    public class Order
    {
        public int OrderId { get; set; }
        public decimal TotalAmount { get; set; }
        public DateTime OrderDate { get; set; } 
        public OrderStatus Status { get; set; }
        public int ApplicationUserId { get; set; }

        public virtual ApplicationUser User { get; set; }
        public virtual ICollection<Message>? Messages { get; set; } = new List<Message>();
        public virtual ICollection<OrderProduct> OrderProducts { get; set; } = new List<OrderProduct>();
        public virtual Shipping? Shipping { get; set; }
        public virtual Payment Payment { get; set; }

    }
    public enum OrderStatus
    {
        Pending,
        Shipped ,
        Delivered,
        Cancelled
    }
}
