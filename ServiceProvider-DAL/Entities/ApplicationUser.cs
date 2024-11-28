using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceProvider_DAL.Entities
{
    public class ApplicationUser : IdentityUser<int>
    {
        public string FullName { get; set; }
        public string Address {  get; set; }
        public DateTime? RegistrationDate { get; set; }
        public DateOnly? BirthDate { get; set; }

        public virtual Cart? Cart { get; set; }
        public virtual ICollection<Message>? Messages { get; set; } = new List<Message>();
        public virtual ICollection<Review>? Reviews { get; set; } = new List<Review>();
        public virtual ICollection<Order>? Orders { get; set; } = new List<Order>();


    }
}
