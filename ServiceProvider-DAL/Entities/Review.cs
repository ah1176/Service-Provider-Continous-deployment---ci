using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceProvider_DAL.Entities
{
    public class Review
    {
        public int ReviewId { get; set; }
        public int Rating { get; set; } 
        public string? Comment { get; set; } 
        public DateTime CreatedAt { get; set; }

        public int ApplicationUserId { get; set; }
        public int ProductId { get; set; }

        public virtual ApplicationUser User { get; set; }
        public virtual Product Product { get; set; }
    }
}
