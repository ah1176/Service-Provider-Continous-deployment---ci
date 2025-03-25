using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using ServiceProvider_BLL.Abstractions;
using ServiceProvider_BLL.Dtos.VendorDto;
using ServiceProvider_BLL.Interfaces;
using ServiceProvider_DAL.Entities;
using System.Threading;

namespace SeeviceProvider_PL.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VendorsController(IUnitOfWork vendorRepositry, UserManager<Vendor> userManager) : ControllerBase
    {
        private readonly IUnitOfWork _vendorRepositry = vendorRepositry;
        private readonly UserManager<Vendor> userManager = userManager;
        [HttpGet("")]
        public async Task<IActionResult> GetProviders(CancellationToken cancellationToken)
        {
            var result = await _vendorRepositry.Vendors.GetAllProviders(cancellationToken);
            return result.IsSuccess
                ? Ok(result.Value)
                : result.ToProblem();
        }

        [HttpGet("{providerId}")]
        public async Task<IActionResult> GetProviderDetalis([FromRoute] string providerId, CancellationToken cancellationToken)
        {
            var result = await _vendorRepositry.Vendors.GetProviderDetails(providerId, cancellationToken);
            return result.IsSuccess
                ? Ok(result.Value)
                : result.ToProblem();
        }


        [HttpGet("{providerId}/menu")]
        public async Task<IActionResult> GetProviderMenu([FromRoute] string providerId, CancellationToken cancellationToken)
        {
            var result = await _vendorRepositry.Vendors.GetProviderMenuAsync(providerId, cancellationToken);
            return result.IsSuccess
                ? Ok(result.Value)
                : result.ToProblem();
        }



        //[Authorize(Roles = "Admin")]
        //[HttpPost("register")]
        //public async Task<IActionResult> RegisterVendor([FromBody] RegisterVendorResponse vendorDto, CancellationToken cancellationToken)
        //{

        //    var result = await _vendorRepositry.Vendors.RegisterVendorAsync(vendorDto, cancellationToken);
        //    return result.IsSuccess
        //         ? Ok(result.Value)
        //         : result.ToProblem();
        //}


        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateVendor(string id, [FromBody] UpdateVendorResponse vendorDto, CancellationToken cancellationToken)
        {

            var result = await _vendorRepositry.Vendors.UpdateVendorAsync(id, vendorDto, cancellationToken);
            return result.IsSuccess
                 ? Ok(result.Value)
                 : result.ToProblem();


          
        }

        [HttpPut("ChangePassword/{id}")]
        public async Task<IActionResult> ChangeVendorPassword(string id, [FromBody] ChangeVendorPasswordResponse vendorDto, CancellationToken cancellationToken)
        {

            var result = await _vendorRepositry.Vendors.ChangeVendorPasswordAsync(id, vendorDto, cancellationToken);
            return result.IsSuccess
                 ? Ok("Password updated successfully")
                 : result.ToProblem();



        }
    }
}

