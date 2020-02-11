using System.Threading.Tasks;
using Newtonsoft.Json;
using Siren.Contracts.Models.Profile;
using Siren.Contracts.Services;

namespace Siren.Services
{
    public class ProfileService : IProfileService
    {
        private readonly IHttpService httpService;

        public ProfileService(IHttpService httpService)
        {
            this.httpService = httpService;
        }

        public async Task<CurrentUserProfileInfo> GetCurrentUserInfo()
        {
            var result = await httpService.GetAsync("Profile/GetCurrentUser", App.Token);
            var userInfo =
                JsonConvert.DeserializeObject<CurrentUserProfileInfo>(await result.Content.ReadAsStringAsync());
            return userInfo;
        }
    }
}
