using Mapster;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using SeeviceProvider_BLL.Abstractions;
using ServiceProvider_BLL.Dtos.ProductDto;
using ServiceProvider_BLL.Dtos.VendorDto;
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
    public class VendorRepository : BaseRepository<Vendor> , IVendorRepository
    {
        private readonly AppDbContext _context;

        public VendorRepository(AppDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<Result<IEnumerable<VendorResponse>>> GetAllProviders(CancellationToken cancellationToken = default)
        {
            var vendors = await _context.Users.AsNoTracking()
                                .ToListAsync(cancellationToken: cancellationToken);
            if (!vendors.Any())
                return Result.Failure<IEnumerable<VendorResponse>>(VendorErrors.NotFound);

            var providers = vendors.Adapt<IEnumerable<VendorResponse>>();

            return Result.Success(providers);
        }

        public async Task<Result<VendorResponse>> GetProviderDetails(string providerId, CancellationToken cancellationToken = default)
        {
            var provider = await _context.Users.AsNoTracking()
                        .FirstOrDefaultAsync(x => x.Id == providerId, cancellationToken);

            if(provider == null)
                return Result.Failure<VendorResponse>(VendorErrors.NotFound);

            return Result.Success(provider.Adapt<VendorResponse>());
        }

        public async Task<Result<IEnumerable<ProductsOfVendorDto>>> GetProviderMenuAsync(string providerId, CancellationToken cancellationToken)
        {
            var providerExists = await _context.Users.AnyAsync(u => u.Id == providerId ,cancellationToken);
            if (!providerExists)
                return Result.Failure<IEnumerable<ProductsOfVendorDto>>(VendorErrors.NotFound);

            var menu = await _context.Products!
                .Where(p => p.VendorId == providerId)
                .ProjectToType<ProductsOfVendorDto>()
                .ToListAsync(cancellationToken);

            return menu.Any()
                ? Result.Success<IEnumerable<ProductsOfVendorDto>>(menu)
                : Result.Failure<IEnumerable<ProductsOfVendorDto>>(ProductErrors.NotFound);
        }
        
    }
}
