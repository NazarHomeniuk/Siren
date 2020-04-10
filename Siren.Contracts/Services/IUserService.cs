using System.Collections.Generic;
using System.Threading.Tasks;
using Siren.Contracts.Models.Profile;
using Siren.Contracts.Models.Suggestion;

namespace Siren.Contracts.Services
{
    public interface IUserService
    {
        Task<IEnumerable<UserSuggestion>> GetUserSuggestions();
        Task<UserProfileInfo> GetUserProfileInfo(string userId);
        Task<bool> Follow(string userId);
        Task<bool> Unfollow(string userId);
        Task<bool> AddTrack(int trackId);
        Task<bool> RemoveTrack(int trackId);
        Task<IEnumerable<Track>> GetCurrentUserTracks();
        Task<IEnumerable<Track>> GetUserTracks(string id);
    }
}