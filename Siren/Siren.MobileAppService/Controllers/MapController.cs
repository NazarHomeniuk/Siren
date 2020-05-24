using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Siren.Contracts.Models.Identity;
using Siren.Contracts.Models.Map;
using Siren.MobileAppService.Interfaces.Services;

namespace Siren.MobileAppService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MapController : ControllerBase
    {
        private readonly UserManager<User> userManager;
        private readonly IMapService mapService;

        public MapController(UserManager<User> userManager, IMapService mapService)
        {
            this.userManager = userManager;
            this.mapService = mapService;
        }

        [HttpPost("UpdateCurrentUserPosition")]
        [Authorize]
        public async Task<IActionResult> UpdateCurrentUserPosition(UserPosition userPosition)
        {
            var user = await userManager.GetUserAsync(User);
            try
            {
                var result = await mapService.UpdateUserPosition(user, userPosition);
                return Ok(result);
            }
            catch (Exception e)
            {
                return BadRequest(e);
            }
        }

        [HttpGet("GetMapUsers")]
        [Authorize]
        public IActionResult GetMapUsers()
        {
            try
            {
                var result = mapService.GetMapUsers();
                return Ok(result);
            }
            catch (Exception e)
            {
                return BadRequest(e);
            }
        }
    }
}