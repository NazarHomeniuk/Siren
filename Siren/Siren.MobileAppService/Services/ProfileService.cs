using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Siren.Contracts.Models.Identity;
using Siren.Contracts.Models.Profile;
using Siren.MobileAppService.Interfaces.Repositories;
using Siren.MobileAppService.Interfaces.Services;

namespace Siren.MobileAppService.Services
{
    public class ProfileService : IProfileService
    {
        private readonly IProfilePhotoRepository profilePhotoRepository;

        public ProfileService(IProfilePhotoRepository profilePhotoRepository)
        {
            this.profilePhotoRepository = profilePhotoRepository;
        }

        public async Task<ProfilePhoto> GetUserPhoto(string userId)
        {
            return await profilePhotoRepository.GetForUser(userId);
        }

        public async Task UpdateUserPhoto(User user, IFormFile file)
        {
            var profilePhoto = await profilePhotoRepository.GetForUser(user.Id);
            byte[] array;
            using (var stream = new MemoryStream())
            {
                await file.CopyToAsync(stream);
                array = stream.ToArray();
            }

            if (profilePhoto == null)
            {
                profilePhoto = new ProfilePhoto
                {
                    UserId = user.Id,
                    Image = array
                };

                await profilePhotoRepository.Create(profilePhoto);
            }
            else
            {
                profilePhoto.Image = array;

                await profilePhotoRepository.Update(profilePhoto);
            }
        }
    }
}
