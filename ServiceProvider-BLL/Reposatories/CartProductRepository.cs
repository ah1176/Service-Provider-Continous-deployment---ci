using Mapster;
using Microsoft.EntityFrameworkCore;
using SeeviceProvider_BLL.Abstractions;
using ServiceProvider_BLL.Dtos.CartProductDto;
using ServiceProvider_BLL.Errors;
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
    public class CartProductRepository : BaseRepository<CartProduct> , ICartProductRepository
    {
        private readonly AppDbContext _context;

        public CartProductRepository(AppDbContext context) : base(context)
        {
            _context = context;
        }

   
    }
}
