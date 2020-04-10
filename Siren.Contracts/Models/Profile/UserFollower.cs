using Siren.Contracts.Models.Identity;

namespace Siren.Contracts.Models.Profile
{
    public class UserFollower
    {
        public int Id { get; set; }
        public User User { get; set; }
        public string UserId { get; set; }
        public User FollowingUser { get; set; }
        public string FollowingUserId { get; set; }
    }
}
