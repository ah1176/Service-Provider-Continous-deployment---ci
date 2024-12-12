using Mapster;
using Microsoft.EntityFrameworkCore;
using SeeviceProvider_BLL.Abstractions;
using ServiceProvider_BLL.Dtos.ProductDto;
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
    public class ProductRepository : BaseRepository<Product> , IProductRepository
    {
        private readonly AppDbContext _context;

        public ProductRepository(AppDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<Result<IEnumerable<ProductsOfVendorDto>>> GetProductsAsync(string vendorId, CancellationToken cancellationToken)
        {
            var menu = await _context.Products!
                .Where(p => p.VendorId == vendorId)
                .Select(p => new
                {
                    p.Id,
                    p.NameEn,
                    p.NameAr,
                    p.Description,
                    p.Price

                }).ToListAsync(cancellationToken);

            return !menu.Any() ?
                Result.Failure<IEnumerable<ProductsOfVendorDto>>(new Error("Not Found", "No menu items found for this provider"))
                : Result.Success(menu.Adapt<IEnumerable<ProductsOfVendorDto>>());
        }
    }
}
