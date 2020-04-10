using System.Threading.Tasks;
using Siren.Contracts.Models.Profile;

namespace Siren.Contracts.Services
{
    public interface IProfileService
    {
        Task<UserProfileInfo> GetCurrentUserInfo();
        Task<bool> UpdateUserPhoto(byte[] image);
    }
}