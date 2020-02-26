using Siren.Contracts.Models.Identity;

namespace Siren.Contracts.Models.Profile
{
    public class ProfilePhoto
    {
        public int Id { get; set; }
        public string UserId { get; set; }
        public User User { get; set; }
        public byte[] Image { get; set; }
    }
}
