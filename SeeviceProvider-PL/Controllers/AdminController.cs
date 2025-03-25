using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ServiceProvider_BLL.Abstractions;
using ServiceProvider_BLL.Interfaces;

namespace SeeviceProvider_PL.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "Admin")]
    public class AdminController(IAuthRepositry authRepositry) : ControllerBase
    {
        private readonly IAuthRepositry _authRepositry = authRepositry;
        

        //[Authorize]
        //[HttpGet("debug-claims")]
        //public IActionResult DebugClaims()
        //{
        //    var claims = User.Claims.Select(c => new { c.Type, c.Value }).ToList();
        //    return Ok(claims);
        //}
    }
}
