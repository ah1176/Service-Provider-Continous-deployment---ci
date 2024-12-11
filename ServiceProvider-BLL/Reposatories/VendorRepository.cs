using ServiceProvider_BLL.Interfaces;
using ServiceProvider_DAL.Data;
using ServiceProvider_DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceProvider_BLL.Reposatories
{
    public class VendorRepository : BaseRepository<Vendor> , IVendorRepository
    {
        private readonly AppDbContext _context;

        public VendorRepository(AppDbContext context) : base(context)
        {
            _context = context;
        }
    }
}
