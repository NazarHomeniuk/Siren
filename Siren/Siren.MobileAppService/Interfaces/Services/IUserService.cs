using System.Collections.Generic;
using System.Threading.Tasks;
using Siren.Contracts.Models.Identity;
using Siren.Contracts.Models.Map;
using Siren.Contracts.Models.Profile;
using Siren.Contracts.Models.Suggestion;

namespace Siren.MobileAppService.Interfaces.Services
{
    public interface IUserService
    {
        IEnumerable<UserSuggestion> GetUserSuggestions();
        Task<UserProfileInfo> GetUserProfileInfo(string id, User user);
        Task Follow(string followerId, string followingId);
        Task Unfollow(string followerId, string followingId);
        Task<bool> AddTrack(string userId, int trackId);
        Task<bool> RemoveTrack(string userId, int trackId);
        IEnumerable<UserFollower> GetUserFollowers(string userId);
        IEnumerable<Track> GetUserTracks(string userId);
    }
}