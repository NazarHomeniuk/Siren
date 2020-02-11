using System.Threading.Tasks;
using Siren.Contracts.Models.Profile;

namespace Siren.Contracts.Services
{
    public interface IProfileService
    {
        Task<CurrentUserProfileInfo> GetCurrentUserInfo();
    }
}