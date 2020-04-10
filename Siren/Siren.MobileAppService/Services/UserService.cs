using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Siren.Contracts.Models.Identity;
using Siren.Contracts.Models.Profile;
using Siren.Contracts.Models.Suggestion;
using Siren.MobileAppService.Interfaces.Repositories;
using Siren.MobileAppService.Interfaces.Services;

namespace Siren.MobileAppService.Services
{
    public class UserService : IUserService
    {
        private readonly IUserFollowerRepository userFollowerRepository;
        private readonly IUserRepository userRepository;
        private readonly ITrackRepository trackRepository;
        private readonly IUserTrackRepository userTrackRepository;

        public UserService(IUserFollowerRepository userFollowerRepository, IUserRepository userRepository,
            ITrackRepository trackRepository, IUserTrackRepository userTrackRepository)
        {
            this.userFollowerRepository = userFollowerRepository;
            this.userRepository = userRepository;
            this.trackRepository = trackRepository;
            this.userTrackRepository = userTrackRepository;
        }

        public IEnumerable<UserSuggestion> GetUserSuggestions()
        {
            var users = userRepository.GetAll();
            return users.Select(u => new UserSuggestion
            {
                UserId = u.Id,
                UserName = u.UserName,
                UserImage = "http://10.0.2.2:40001/api/profile/getuserphoto?id=" + u.Id
            });
        }

        public async Task<UserProfileInfo> GetUserProfileInfo(string id, User user)
        {
            var userInfo = await userRepository.GetWithAdditionalInfo(id);
            var track = new Track();
            if (userInfo.TrackId.HasValue)
            {
                track = await trackRepository.Get(userInfo.TrackId.Value);
            }

            var isFollowed = userFollowerRepository.GetAll()
                .Any(f => f.UserId == user.Id && f.FollowingUserId == userInfo.Id);

            var followedCount = userFollowerRepository.GetAll().Count(f => f.FollowingUserId == id);
            var followingsCount = userFollowerRepository.GetAll().Count(f => f.UserId == id);
            var tracksCount = userTrackRepository.GetAll().Count(t => t.UserId == id);

            return new UserProfileInfo
            {
                Email = userInfo.Email,
                ImagePath = "http://10.0.2.2:40001/api/profile/getuserphoto?id=" + userInfo.Id,
                UserName = userInfo.UserName,
                TrackArtist = track.Artist,
                TrackTitle = track.Title,
                IsFollowed = isFollowed,
                FollowedCount = followedCount,
                FollowersCount = followingsCount,
                TracksCount = tracksCount
            };
        }

        public async Task Follow(string followerId, string followingId)
        {
            var userFollower = new UserFollower
            {
                UserId = followerId,
                FollowingUserId = followingId
            };
            await userFollowerRepository.Create(userFollower);
        }

        public async Task Unfollow(string followerId, string followingId)
        {
            var userFollower = userFollowerRepository.GetAll()
                .Where(u => u.UserId == followerId && u.FollowingUserId == followingId);
            await userFollowerRepository.DeleteAll(userFollower);
        }

        public async Task<bool> AddTrack(string userId, int trackId)
        {
            var userTrack = userTrackRepository.GetAll()
                .FirstOrDefault(t => t.UserId == userId && t.TrackId == trackId);
            if (userTrack == null)
            {
                userTrack = new UserTrack
                {
                    UserId = userId,
                    TrackId = trackId
                };

                await userTrackRepository.Create(userTrack);
                return true;
            }

            return false;
        }

        public async Task<bool> RemoveTrack(string userId, int trackId)
        {
            var userTrack = userTrackRepository.GetAll()
                .FirstOrDefault(t => t.UserId == userId && t.TrackId == trackId);
            if (userTrack != null)
            {
                await userTrackRepository.Delete(userTrack.Id);
                return true;
            }

            return false;
        }

        public IEnumerable<UserFollower> GetUserFollowers(string userId)
        {
            return userFollowerRepository.GetAll().Where(f => f.UserId == userId);
        }

        public IEnumerable<Track> GetUserTracks(string userId)
        {
            return userTrackRepository.GetAll().Where(t => t.UserId == userId).Select(t => t.Track);
        }
    }
}