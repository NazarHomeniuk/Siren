using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Siren.Contracts.Models.Identity;
using Siren.MobileAppService.Interfaces.Services;

namespace Siren.MobileAppService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AudioController : ControllerBase
    {
        private readonly UserManager<User> userManager;
        private readonly ITrackService trackService;

        public AudioController(UserManager<User> userManager, ITrackService trackService)
        {
            this.userManager = userManager;
            this.trackService = trackService;
        }

        [Authorize]
        [HttpGet("getAll")]
        public IActionResult GetAll()
        {
            return Ok(trackService.GetAll());
        }

        [Authorize]
        [HttpGet("getAllIds")]
        public IActionResult GetAllIds()
        {
            return Ok(trackService.GetAllIds());
        }

        [HttpGet("playTrack")]
        public async Task<IActionResult> PlayTrack(int id)
        {
            var user = await userManager.GetUserAsync(User);
            var track = await trackService.Play(id, user);
            return File(track.Data, "audio/mpeg");
        }

        [HttpPost("upload")]
        public async Task<IActionResult> Upload(string path)
        {
            try
            {
                await trackService.Upload(path);
                return Ok();
            }
            catch (Exception e)
            {
                return BadRequest(e);
            }
        }
    }
}