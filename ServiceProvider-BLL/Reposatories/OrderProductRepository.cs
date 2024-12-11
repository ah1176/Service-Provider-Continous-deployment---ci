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
    public class OrderProductRepository : BaseRepository<OrderProduct> , IOrderProductRepository
    {
        private readonly AppDbContext _context;

        public OrderProductRepository(AppDbContext context) : base(context)
        {
            _context = context;
        }
    }
}
