using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using ServiceProvider_DAL.Entities;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceProvider_DAL.Data
{
    public class AppDbContext : IdentityDbContext<ApplicationUser, IdentityRole<int>,int>
    {

    }
}
