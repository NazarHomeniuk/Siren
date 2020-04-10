using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Siren.Contracts.Models.Profile;
using Siren.Contracts.Models.Suggestion;
using Siren.Contracts.Services;

namespace Siren.Services
{
    public class UserService : IUserService
    {
        private readonly IHttpService httpService;

        public UserService(IHttpService httpService)
        {
            this.httpService = httpService;
        }
        
        public async Task<IEnumerable<UserSuggestion>> GetUserSuggestions()
        {
            var response = await httpService.GetAsync("User/GetUserSuggestions", App.Token);
            var suggestions =
                JsonConvert.DeserializeObject<IEnumerable<UserSuggestion>>(await response.Content.ReadAsStringAsync());
            return suggestions;
        }

        public async Task<UserProfileInfo> GetUserProfileInfo(string userId)
        {
            var response = await httpService.GetAsync($"User/GetUserProfileInfo?id={userId}", App.Token);
            var profileInfo =
                JsonConvert.DeserializeObject<UserProfileInfo>(await response.Content.ReadAsStringAsync());
            return profileInfo;
        }

        public async Task<bool> Follow(string userId)
        {
            var response = await httpService.PostAsync($"User/Follow?followingId={userId}", new StringContent(userId),
                App.Token);
            return response.IsSuccessStatusCode;
        }

        public async Task<bool> Unfollow(string userId)
        {
            var response = await httpService.PostAsync($"User/Unfollow?followingId={userId}", new StringContent(userId),
                App.Token);
            return response.IsSuccessStatusCode;
        }

        public async Task<bool> AddTrack(int trackId)
        {
            var response = await httpService.PostAsync($"User/AddTrack?trackId={trackId}", new StringContent(trackId.ToString()),
                App.Token);
            if (response.IsSuccessStatusCode)
            {
                var result = JsonConvert.DeserializeObject<bool>(await response.Content.ReadAsStringAsync());
                return result;
            }

            return false;
        }

        public async Task<bool> RemoveTrack(int trackId)
        {
            var response = await httpService.PostAsync($"User/RemoveTrack?trackId={trackId}", new StringContent(trackId.ToString()),
                App.Token);
            if (response.IsSuccessStatusCode)
            {
                var result = JsonConvert.DeserializeObject<bool>(await response.Content.ReadAsStringAsync());
                return result;
            }

            return false;
        }

        public async Task<IEnumerable<Track>> GetCurrentUserTracks()
        {
            var response = await httpService.GetAsync("User/GetUserTracks", App.Token);
            var result = JsonConvert.DeserializeObject<IEnumerable<Track>>(await response.Content.ReadAsStringAsync());
            return result;
        }

        public async Task<IEnumerable<Track>> GetUserTracks(string id)
        {
            var response = await httpService.GetAsync($"User/GetUserTracks?userId={id}", App.Token);
            var result = JsonConvert.DeserializeObject<IEnumerable<Track>>(await response.Content.ReadAsStringAsync());
            return result;
        }
    }
}
