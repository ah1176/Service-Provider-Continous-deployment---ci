using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceProvider_DAL.Entities
{
    public class Message
    {
        public int MessageId { get; set; }
        public string MessageText { get; set; } 
        public DateTime MessageDate { get; set; } 
        public bool? IsRead {  get; set; }
        public string ApplicationUserId { get; set; }
        public string VendorId { get; set; }
        public int OrderId { get; set; }

        public ApplicationUser User { get; set; }
        public Vendor Vendor { get; set; }
        public Order Order { get; set; }
    }
}
