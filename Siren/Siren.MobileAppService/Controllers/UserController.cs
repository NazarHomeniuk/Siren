using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Siren.Contracts.Models.Identity;
using Siren.Contracts.Models.Profile;
using Siren.Contracts.Models.Suggestion;
using Siren.MobileAppService.Interfaces.Services;

namespace Siren.MobileAppService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService userService;
        private readonly UserManager<User> userManager;

        public UserController(IUserService userService, UserManager<User> userManager)
        {
            this.userService = userService;
            this.userManager = userManager;
        }

        [Authorize]
        [HttpGet("GetUserSuggestions")]
        public IEnumerable<UserSuggestion> GetUserSuggestions()
        {
            return userService.GetUserSuggestions();
        }

        [Authorize]
        [HttpGet("GetUserProfileInfo")]
        public async Task<UserProfileInfo> GetUserProfileInfo(string id)
        {
            var user = await userManager.GetUserAsync(User);
            return await userService.GetUserProfileInfo(id, user);
        }

        [Authorize]
        [HttpPost("AddTrack")]
        public async Task<IActionResult> AddTrack(int trackId)
        {
            var user = await userManager.GetUserAsync(User);
            try
            {
                var result = await userService.AddTrack(user.Id, trackId);
                return Ok(result);
            }
            catch (Exception e)
            {
                return BadRequest(e);
            }
        }

        [Authorize]
        [HttpPost("RemoveTrack")]
        public async Task<IActionResult> RemoveTrack(int trackId)
        {
            var user = await userManager.GetUserAsync(User);
            try
            {
                var result = await userService.RemoveTrack(user.Id, trackId);
                return Ok(result);
            }
            catch (Exception e)
            {
                return BadRequest(e);
            }
        }

        [Authorize]
        [HttpPost("Follow")]
        public async Task<IActionResult> Follow(string followingId)
        {
            var user = await userManager.GetUserAsync(User);
            try
            {
                await userService.Follow(user.Id, followingId);
                return Ok();
            }
            catch (Exception e)
            {
                return BadRequest(e);
            }
        }

        [Authorize]
        [HttpPost("Unfollow")]
        public async Task<IActionResult> Unfollow(string followingId)
        {
            var user = await userManager.GetUserAsync(User);
            try
            {
                await userService.Unfollow(user.Id, followingId);
                return Ok();
            }
            catch (Exception e)
            {
                return BadRequest(e);
            }
        }

        [Authorize]
        [HttpGet("GetUserFollowers")]
        public async Task<IActionResult> GetUserFollowers()
        {
            var user = await userManager.GetUserAsync(User);
            try
            {
                var userFollowers = userService.GetUserFollowers(user.Id);
                return Ok(userFollowers);
            }
            catch (Exception e)
            {
                return BadRequest(e);
            }
        }

        [Authorize]
        [HttpGet("GetUserFollowers")]
        public IActionResult GetUserFollowers(string userId)
        {
            try
            {
                var userFollowers = userService.GetUserFollowers(userId);
                return Ok(userFollowers);
            }
            catch (Exception e)
            {
                return BadRequest(e);
            }
        }

        [Authorize]
        [HttpGet("GetUserTracks")]
        public async Task<IActionResult> GetUserTracks(string userId)
        {
            try
            {
                var userTracks = new List<Track>();
                if (string.IsNullOrEmpty(userId))
                {
                    var user = await userManager.GetUserAsync(User);
                    userTracks = userService.GetUserTracks(user.Id).ToList();
                }
                else
                {
                    userTracks = userService.GetUserTracks(userId).ToList();

                }

                return Ok(userTracks);
            }
            catch (Exception e)
            {
                return BadRequest(e);
            }
        }
    }
}