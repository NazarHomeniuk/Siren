using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Siren.MobileAppService.Interfaces.Services;

namespace Siren.MobileAppService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AudioController : ControllerBase
    {
        private readonly ITrackService trackService;

        public AudioController(ITrackService trackService)
        {
            this.trackService = trackService;
        }

        [Authorize]
        [HttpGet("getAllIds")]
        public IActionResult GetAllIds()
        {
            return Ok(trackService.GetAllIds());
        }

        [HttpGet("getTrack")]
        public async Task<IActionResult> GetTrack(int id)
        {
            var track = await trackService.Get(id);
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