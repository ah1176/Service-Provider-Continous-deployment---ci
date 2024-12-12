using Mapster;
using Microsoft.EntityFrameworkCore;
using SeeviceProvider_BLL.Abstractions;
using ServiceProvider_BLL.Dtos.VendorDto;
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

        public async Task<Result<IEnumerable<VendorResponse>>> GetVendorsByCategoryIdAsync(int categoryId, CancellationToken cancellationToken)
        {
            var vendors = await _context.Users
                            .Where(v => v.VendorSubCategories!.Any(vc => vc.SubCategory.CategoryId == categoryId))
                            .Select(v => new
                            {
                                v.Id,
                                v.FullName,
                                v.BusinessType,
                                v.Rating,
                            }).ToListAsync(cancellationToken);

            return !vendors.Any() ?
                Result.Failure<IEnumerable<VendorResponse>>(new Error("Not Found", "no vendors found in this category"))
                : Result.Success(vendors.Adapt<IEnumerable<VendorResponse>>());
        }
    }
}
