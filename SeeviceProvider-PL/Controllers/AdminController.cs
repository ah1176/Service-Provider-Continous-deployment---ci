using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ServiceProvider_BLL.Abstractions;
using ServiceProvider_BLL.Dtos.Common;
using ServiceProvider_BLL.Interfaces;

namespace SeeviceProvider_PL.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "Admin")]
    public class AdminController(IAnalyticsRepositry analyticsRepositry) : ControllerBase
    {
        private readonly IAnalyticsRepositry _analyticsRepositry = analyticsRepositry;


        [HttpGet("today-stats")]
        public async Task<IActionResult> GetTodayStats(CancellationToken cancellationToken = default) 
        {
            var result = await _analyticsRepositry.GetTodaysStatsAsync(cancellationToken);

            return Ok(result.Value);
        }

        [HttpGet("top-vendors")]
        public async Task<IActionResult> GetTopVendors()
        {
            var result = await _analyticsRepositry.GetTopVendorsAsync();

            return Ok(result.Value);
        }


        [HttpGet("project-summary")]
        public async Task<IActionResult> GetProjectSummary()
        {
            var result = await _analyticsRepositry.GetOverallStatisticsAsync();

            return Ok(result.Value);
        }



        //[Authorize]
        //[HttpGet("debug-claims")]
        //public IActionResult DebugClaims()
        //{
        //    var claims = User.Claims.Select(c => new { c.Type, c.Value }).ToList();
        //    return Ok(claims);
        //}
    }
}
