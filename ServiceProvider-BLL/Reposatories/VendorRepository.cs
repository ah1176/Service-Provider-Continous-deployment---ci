using Mapster;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Identity;
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
    public class VendorRepository : BaseRepository<Vendor>, IVendorRepository
    {
        private readonly AppDbContext _context;
        private readonly UserManager<Vendor> _usermanager;
        private readonly IPasswordHasher<Vendor> _passwordHasher;
        public VendorRepository(AppDbContext context, UserManager<Vendor> userManager, IPasswordHasher<Vendor> passwordHasher) : base(context)
        {
            _context = context;
            _usermanager = userManager;
            _passwordHasher = passwordHasher;
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

            if (provider == null)
                return Result.Failure<VendorResponse>(VendorErrors.NotFound);

            return Result.Success(provider.Adapt<VendorResponse>());
        }

        public async Task<Result<IEnumerable<ProductsOfVendorDto>>> GetProviderMenuAsync(string providerId, CancellationToken cancellationToken)
        {
            var providerExists = await _context.Users.AnyAsync(u => u.Id == providerId, cancellationToken);
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
       

        public async Task<Result<UpdateVendorResponse>> UpdateVendorAsync(string id,UpdateVendorResponse vendorDto, CancellationToken cancellationToken = default)
        {

            var vendor = await _usermanager.FindByIdAsync(id);
            if (vendor == null )
                return Result.Failure<UpdateVendorResponse>(VendorErrors.NotFound);
            
            vendor.UserName = vendorDto.UserName;
            if (vendor.FullName == null)
                vendor.FullName = vendorDto.UserName;

            vendor.BusinessName = vendorDto.BusinessName;

            var result = await _usermanager.UpdateAsync(vendor);
            await _context.SaveChangesAsync();
            return Result.Success(vendor.Adapt<UpdateVendorResponse>());
        }

        public async Task<Result<ChangeVendorPasswordResponse>> ChangeVendorPasswordAsync(string id, ChangeVendorPasswordResponse vendorDto, CancellationToken cancellationToken = default)
        {

            var vendor = await _usermanager.FindByIdAsync(id);
            if (vendor == null)
                return Result.Failure<ChangeVendorPasswordResponse>(VendorErrors.NotFound);


            var passwordValid = await _usermanager.CheckPasswordAsync(vendor, vendorDto.OldPassword);
            if (!passwordValid)
            {
                return Result.Failure<ChangeVendorPasswordResponse>(VendorErrors.IncorrectPassword);
            }


            await _usermanager.ChangePasswordAsync(vendor, vendorDto.OldPassword, vendorDto.NewPassword);
            await _context.SaveChangesAsync();
          
            return Result.Success(vendor.Adapt<ChangeVendorPasswordResponse>());
        }


    }
}
