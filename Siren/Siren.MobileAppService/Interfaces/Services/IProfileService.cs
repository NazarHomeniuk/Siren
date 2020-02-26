using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Siren.Contracts.Models.Identity;
using Siren.Contracts.Models.Profile;

namespace Siren.MobileAppService.Interfaces.Services
{
    public interface IProfileService
    {
        Task<ProfilePhoto> GetUserPhoto(string userId);
        Task UpdateUserPhoto(User user, IFormFile file);
    }
}