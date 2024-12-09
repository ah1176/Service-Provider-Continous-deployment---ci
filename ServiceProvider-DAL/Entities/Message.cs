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
        public string MessageText { get; set; } = string.Empty;
        public DateTime MessageDate { get; set; } 
        public bool? IsRead {  get; set; }
        public string ApplicationUserId { get; set; } = string.Empty;
        public string VendorId { get; set; } = string.Empty ;
        public int OrderId { get; set; }

        public ApplicationUser User { get; set; } = default!;
        public Vendor Vendor { get; set; } = default!;
        public Order Order { get; set; } = default!;
    }
}
