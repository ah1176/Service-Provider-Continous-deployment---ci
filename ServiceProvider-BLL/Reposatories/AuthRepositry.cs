using Microsoft.AspNetCore.Identity;
using SeeviceProvider_BLL.Abstractions;
using ServiceProvider_BLL.Authentication;
using ServiceProvider_BLL.Dtos.AuthenticationDto;
using ServiceProvider_BLL.Errors;
using ServiceProvider_BLL.Interfaces;
using ServiceProvider_DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceProvider_BLL.Reposatories
{
    public class AuthRepositry(UserManager<Vendor> userManager,IJwtProvider jwtProvider) : IAuthRepositry
    {
        private readonly UserManager<Vendor> _userManager = userManager;
        private readonly IJwtProvider _jwtProvider = jwtProvider;

        public async Task<Result<AuthResponse>> GetTokenAsync(string email, string password, CancellationToken cancellation)
        {
            var vendor = await _userManager.FindByEmailAsync(email);

            if (vendor == null)
                return Result.Failure<AuthResponse>(VendorErrors.InvalidCredentials);

            var isValidPassword = await _userManager.CheckPasswordAsync(vendor, password);

            if (!isValidPassword)
                return Result.Failure<AuthResponse>(VendorErrors.InvalidCredentials);


            var (token, expiresIn) = _jwtProvider.GenerateToken(vendor);

            var response =  new AuthResponse(Guid.NewGuid().ToString(),"admin@test.com","AhmedTahoon","ElecPoss","Elec","jvfjkjjdjndndcjf",3600);

            return Result.Success(response);
        }
    }
}
