using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using ServiceProvider_DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace ServiceProvider_BLL.Authentication.Filters
{
    public class CustomUserClaimsPrincipalFactory : UserClaimsPrincipalFactory<Vendor>
    {
        public CustomUserClaimsPrincipalFactory(UserManager<Vendor> userManager, IOptions<IdentityOptions> optionsAccessor)
            : base(userManager, optionsAccessor) { }

        public override async Task<ClaimsPrincipal> CreateAsync(Vendor user)
        {
            var principal = await base.CreateAsync(user);
            var identity = (ClaimsIdentity)principal.Identity!;
            identity!.AddClaim(new Claim("IsApproved", user.IsApproved.ToString()));
            return principal;
        }
    }

}
