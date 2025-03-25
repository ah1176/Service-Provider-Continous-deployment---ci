using Mapster;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using SeeviceProvider_BLL.Abstractions;
using ServiceProvider_BLL.Authentication;
using ServiceProvider_BLL.Dtos.AuthenticationDto;
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
    public class AuthRepositry(
        UserManager<Vendor> userManager
        ,IJwtProvider jwtProvider
        ,AppDbContext context) : IAuthRepositry
    {
        private readonly UserManager<Vendor> _userManager = userManager;
        private readonly IJwtProvider _jwtProvider = jwtProvider;
        private readonly AppDbContext _context = context;

        public async Task<Result<AuthResponse>> GetTokenAsync(string email, string password, CancellationToken cancellation)
        {
            var vendor = await _userManager.FindByEmailAsync(email);

            if (vendor == null)
                return Result.Failure<AuthResponse>(VendorErrors.InvalidCredentials);

            var isValidPassword = await _userManager.CheckPasswordAsync(vendor, password);

            if (!isValidPassword)
                return Result.Failure<AuthResponse>(VendorErrors.InvalidCredentials);

            if (await _userManager.IsInRoleAsync(vendor, "Vendor"))
            {
                if (!vendor.IsApproved)
                    return Result.Failure<AuthResponse>(VendorErrors.NotApproved);

                if (!vendor.EmailConfirmed)
                    return Result.Failure<AuthResponse>(VendorErrors.EmailNotConfirmed);
            }

            var roles = await _userManager.GetRolesAsync(vendor);

            var (token, expiresIn) = _jwtProvider.GenerateToken(vendor, roles);

            var response =  new AuthResponse(vendor.Id,vendor.Email,vendor.FullName,vendor.BusinessName,vendor.BusinessType,token,expiresIn);

            return Result.Success(response);
        }

        public async Task<Result> RegisterAsync(RegisterationRequest request, CancellationToken cancellationToken = default) 
        {
            if (await _userManager.Users.AnyAsync(x => x.Email == request.Email, cancellationToken))
                return Result.Failure(VendorErrors.DuplicatedEmail);

            using var transaction = await _context.Database.BeginTransactionAsync(cancellationToken);

            try
            {
                var vendor = new Vendor
                {
                    UserName = request.Email,
                    Email = request.Email,
                    FullName = request.FullName,
                    BusinessName = request.BusinessName,
                    BusinessType = request.BusinessType,
                    TaxNumber = request.TaxNumber,
                    IsApproved = false // Vendor starts as not approved
                };

                var result = await _userManager.CreateAsync(vendor, request.Password);
                if (!result.Succeeded)
                {
                    var error = result.Errors.First();

                    return Result.Failure(new Error(error.Code, error.Description, StatusCodes.Status400BadRequest));
                }

                await _userManager.AddToRoleAsync(vendor, "Vendor");

               
                var existingSubCategoryIds = await _context.SubCategories!
                    .Where(s => request.SubCategoryIds.Contains(s.Id))
                    .Select(s => s.Id)
                    .ToListAsync(cancellationToken);

                var invalidSubCategories = request.SubCategoryIds.Except(existingSubCategoryIds).ToList();
                if (invalidSubCategories.Any())
                    return Result.Failure(new Error("InvalidSubCategory", $"Invalid Subcategories: {string.Join(", ", invalidSubCategories)}", StatusCodes.Status400BadRequest));

                
                var vendorSubCategories = request.SubCategoryIds.Select(subCategoryId => new VendorSubCategory
                {
                    VendorId = vendor.Id,
                    SubCategoryId = subCategoryId
                }).ToList();

                await _context.VendorSubCategories!.AddRangeAsync(vendorSubCategories, cancellationToken);
                await _context.SaveChangesAsync(cancellationToken);

                await transaction.CommitAsync(cancellationToken);

                return Result.Success();
            }
            catch (Exception ex)
            {
                await transaction.RollbackAsync(cancellationToken); 
                return Result.Failure(new Error("TransactionError", ex.Message, StatusCodes.Status500InternalServerError));
            }
        }


    }
}
