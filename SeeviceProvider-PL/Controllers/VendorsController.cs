using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ServiceProvider_BLL.Abstractions;
using ServiceProvider_BLL.Interfaces;

namespace SeeviceProvider_PL.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VendorsController(IUnitOfWork vendorRepositry) : ControllerBase
    {
        private readonly IUnitOfWork _vendorRepositry = vendorRepositry;

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
            var result = await _vendorRepositry.Vendors.GetProviderDetails(providerId ,cancellationToken);
            return result.IsSuccess
                ? Ok(result.Value)
                : result.ToProblem();
        }


        [HttpGet("{providerId}/menu")]
        public async Task<IActionResult> GetProviderMenu([FromRoute]string providerId , CancellationToken cancellationToken)
        {
            var result = await _vendorRepositry.Vendors.GetProviderMenuAsync(providerId ,cancellationToken);
            return result.IsSuccess
                ? Ok(result.Value)
                : result.ToProblem();
        }
    }
}
