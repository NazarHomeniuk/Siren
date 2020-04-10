namespace Siren.Contracts.Models.Profile
{
    public class UserProfileInfo
    {
        public string ImagePath { get; set; }
        public string Email { get; set; }
        public string UserName { get; set; }
        public string TrackArtist { get; set; }
        public string TrackTitle { get; set; }
        public int FollowersCount { get; set; }
        public int FollowedCount { get; set; }
        public int TracksCount { get; set; }
        public bool IsFollowed { get; set; }
    }
}
