using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceProvider_DAL.Entities
{
    public class Cart
    {
        public int Id { get; set; }

        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }

        public string ApplicationUserId { get; set; } = string.Empty;
        public virtual ApplicationUser User { get; set; } = default!;

        public virtual ICollection<CartProduct> CartProducts { get; set; } = new List<CartProduct>();

    }
}
