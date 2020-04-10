using Siren.Contracts.Models.Identity;

namespace Siren.Contracts.Models.Profile
{
    public class UserTrack
    {
        public int Id { get; set; }
        public string UserId { get; set; }
        public User User { get; set; }
        public int TrackId { get; set; }
        public Track Track { get; set; }
    }
}
