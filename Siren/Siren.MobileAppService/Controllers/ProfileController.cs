using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using Siren.Contracts.Models.Identity;
using Siren.Contracts.Models.Profile;
using Siren.MobileAppService.Interfaces.Services;

namespace Siren.MobileAppService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProfileController : ControllerBase
    {
        private readonly UserManager<User> userManager;
        private readonly IProfileService profileService;
        
        public ProfileController(UserManager<User> userManager, IProfileService profileService)
        {
            this.userManager = userManager;
            this.profileService = profileService;
        }

        [Authorize]
        [HttpGet]
        [Route("GetCurrentUser")]
        public async Task<IActionResult> GetCurrentUser()
        {
            var user = await userManager.GetUserAsync(User);
            var currentUserProfileInfo = new CurrentUserProfileInfo
            {
                Email = user.Email,
                ImagePath = "http://10.0.2.2:40001/api/profile/getuserphoto?id=" + user.Id,
                UserName = user.UserName
            };

            return Ok(currentUserProfileInfo);
        }

        [HttpGet]
        [Route("GetUserPhoto")]
        public async Task<IActionResult> GetUserPhoto(string id)
        {
            var profilePhoto = await profileService.GetUserPhoto(id);
            return File(profilePhoto.Image, "image/jpeg");
        }

        [Authorize]
        [HttpPost]
        [Route("UpdateUserPhoto")]
        public async Task<IActionResult> UpdateUserPhoto()
        {
            var user = await userManager.GetUserAsync(User);
            var form = await Request.ReadFormAsync();
            var file = form.Files["image"];
            await profileService.UpdateUserPhoto(user, file);
            return Ok();
        }
    }
}